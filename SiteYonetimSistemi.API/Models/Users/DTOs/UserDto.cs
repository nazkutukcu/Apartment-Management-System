using SiteYonetimSistemi.API.Models.Apartments;
using SiteYonetimSistemi.API.Models.Payments;

namespace SiteYonetimSistemi.API.Models.Users.DTOs
{
    public class UserDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IdentityNumber { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
