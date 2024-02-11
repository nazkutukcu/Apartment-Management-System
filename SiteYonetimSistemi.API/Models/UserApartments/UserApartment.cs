using SiteYonetimSistemi.API.Models.Apartments;
using SiteYonetimSistemi.API.Models.Users;

namespace SiteYonetimSistemi.API.Models.UserApartments
{
    public class UserApartment
    {
        public int Id { get; set; }
        public Guid UserId { get; set; } 
        public User User { get; set; } 
        public int ApartmentId { get; set; } 
        public Apartment Apartment { get; set; } 
    }
}
