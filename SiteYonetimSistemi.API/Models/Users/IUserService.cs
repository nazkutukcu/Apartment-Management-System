using SiteYonetimSistemi.API.Models.Users.DTOs;

namespace SiteYonetimSistemi.API.Models.Users
{
    public interface IUserService
    {
        List<UserDto> GetAll();
        Task<ResponseDto<Guid?>> CreateUser(UserCreateRequestDto request);
        Task<ResponseDto<bool>> Update(UserUpdateRequestDto request);
        Task<ResponseDto<bool>> Delete(Guid userId);
    }
}
