using AutoMapper;
using SiteYonetimSistemi.API.Models.UserApartments.DTOs;
using SiteYonetimSistemi.API.Models.Users.DTOs;
using SiteYonetimSistemi.API.Models.Users;
using SiteYonetimSistemi.API.Models.Apartments;
using SiteYonetimSistemi.API.Models.UnitOfWorks;

namespace SiteYonetimSistemi.API.Models.UserApartments
{
    public class UserApartmentService(IUserApartmentRepository userApartmentRepository,IMapper mapper, 
        IUserRepository userRepository ,IApartmentRepository apartmentRepository, IUnitOfWork unitOfWork) : IUserApartmentService
    {
        public ResponseDto<int> AssignUserToApartment(UserApartmentDto request)
        {
            using (var transaction = unitOfWork.BeginTransaction())
            {
                var apartment = apartmentRepository.GetApartmentById(request.ApartmentId);
                if (apartment == null)
                {
                    return ResponseDto<int>.Fail("Belirtilen daire bulunamadı. Lütfen geçerli bir daire ID'si girin.");
                }

                var user = userRepository.GetById(request.UserId);
                if (user == null)
                {
                    return ResponseDto<int>.Fail("Belirtilen kullanıcı bulunamadı. Lütfen geçerli bir kullanıcı ID'si girin.");
                }

                if (apartment.IsFull)
                {
                    return ResponseDto<int>.Fail("Daire dolu! Lütfen boş bir daire seçin.");
                }

                var existingUserApartment = userApartmentRepository.GetByUserId(request.UserId);
                if (existingUserApartment != null)
                {
                    return ResponseDto<int>.Fail("Kullanıcı zaten bir daireye atanmış.");
                }

                var userApartment = new UserApartment
                {
                    UserId = request.UserId,
                    ApartmentId = request.ApartmentId,
                };

                userApartmentRepository.Add(userApartment);
                unitOfWork.Commit();

                apartment.IsFull = true;
                apartmentRepository.Update(apartment);
                unitOfWork.Commit();

                transaction.Commit();

                return ResponseDto<int>.Success(userApartment.Id);
         
            }
        
        }
               
          
    }

}


