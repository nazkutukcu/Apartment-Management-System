using AutoMapper;
using Azure.Core;
using Microsoft.AspNetCore.Identity;
using SiteYonetimSistemi.API.Models.Apartments;
using SiteYonetimSistemi.API.Models.UnitOfWorks;
using SiteYonetimSistemi.API.Models.Users.DTOs;
using System.ComponentModel.DataAnnotations;

namespace SiteYonetimSistemi.API.Models.Users
{
    public class UserService(IUserRepository userRepository,IMapper mapper,IApartmentRepository apartmentRepository,
        IUnitOfWork unitOfWork, UserManager<User> userManager) : IUserService
    {

        public List<UserDto> GetAll()
        {
            var users = userRepository.GetAll();

            return mapper.Map<List<UserDto>>(users);    
        }

        public async Task<ResponseDto<Guid?>> CreateUser(UserCreateRequestDto request)
        {
            var user = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                IdentityNumber = request.IdentityNumber,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                UserName = request.FirstName + request.LastName,
            };

            var result = await userManager.CreateAsync(user);
            await userManager.AddToRoleAsync(user, "User");

            if (!result.Succeeded)
            {
                var errorList = result.Errors.Select(x => x.Description).ToList();
                return ResponseDto<Guid?>.Fail(errorList);
            }
            return ResponseDto<Guid?>.Success(user.Id);
        }

        public async Task<ResponseDto<bool>> Update(UserUpdateRequestDto request)
        {
            var user = await userRepository.GetByIdAsync(request.UserId);
            if (user == null)
            {
                return ResponseDto<bool>.Fail("Girdiğiniz id de bir kullanıcı bulunamadı.");
            }

            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.IdentityNumber = request.IdentityNumber;
            user.Email = request.Email;
            user.PhoneNumber = request.PhoneNumber;
            user.UserName = request.FirstName + request.LastName;

            userRepository.Update(user);

            return ResponseDto<bool>.Success(true);
        }

        public async Task<ResponseDto<bool>> Delete(Guid userId)
        {
            var user = await userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                return ResponseDto<bool>.Fail("Girdiğiniz ID ile kullanıcı bulunamadı.");
            }

            userRepository.Delete(user.Id);
            return ResponseDto<bool>.Success(true);
        }
    }
}
