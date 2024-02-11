using AutoMapper;
using SiteYonetimSistemi.API.Models.Apartments;
using SiteYonetimSistemi.API.Models.Apartments.DTOs;
using SiteYonetimSistemi.API.Models.Payments;
using SiteYonetimSistemi.API.Models.Payments.DTOs;
using SiteYonetimSistemi.API.Models.UserApartments;
using SiteYonetimSistemi.API.Models.UserApartments.DTOs;
using SiteYonetimSistemi.API.Models.Users;
using SiteYonetimSistemi.API.Models.Users.DTOs;

namespace SiteYonetimSistemi.API.Mapping
{
    public class DtoProfile : Profile
    {
        public DtoProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<Apartment, ApartmentDto>();
            //CreateMap<UserApartment, UserApartmentDto>();
            CreateMap<Payment, PaymentDto>();
        }
    }
}
