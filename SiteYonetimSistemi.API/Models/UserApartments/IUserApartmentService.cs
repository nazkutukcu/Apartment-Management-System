using SiteYonetimSistemi.API.Models.UserApartments.DTOs;

namespace SiteYonetimSistemi.API.Models.UserApartments
{
    public interface IUserApartmentService
    {
        ResponseDto<int> AssignUserToApartment(UserApartmentDto request);

    }
}
