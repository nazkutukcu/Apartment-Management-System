using Microsoft.EntityFrameworkCore;
using SiteYonetimSistemi.API.Models.Users;

namespace SiteYonetimSistemi.API.Models.UserApartments
{
    public class UserApartmentRepository(AppDbContext context) : IUserApartmentRepository
    {
        public UserApartment Add(UserApartment userApartment)
        {
            context.UserApartments.Add(userApartment);
            context.SaveChanges();
            return userApartment;
        }
        public UserApartment GetByUserId(Guid userId)
        {
            return context.UserApartments.FirstOrDefault(ua => ua.UserId == userId);
        }
    }
}
