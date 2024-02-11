namespace SiteYonetimSistemi.API.Models.UserApartments
{
    public interface IUserApartmentRepository
    {
        UserApartment Add(UserApartment userApartment);
        UserApartment GetByUserId(Guid userId);
    }
}
