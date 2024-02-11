using Microsoft.AspNetCore.Identity;
using SiteYonetimSistemi.API.Models.Apartments;
using SiteYonetimSistemi.API.Models.Payments;
using SiteYonetimSistemi.API.Models.UserApartments;

namespace SiteYonetimSistemi.API.Models.Users
{
    public class User : IdentityUser<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IdentityNumber { get; set; } 
        public string Email { get; set; } 
        public string PhoneNumber { get; set; } 

        // The relationship between the user and apartment.
        public UserApartment? UserApartment { get; set; } 
    }
}

