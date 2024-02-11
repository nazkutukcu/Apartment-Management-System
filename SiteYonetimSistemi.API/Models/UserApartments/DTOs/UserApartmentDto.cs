using SiteYonetimSistemi.API.Models.Apartments;
using SiteYonetimSistemi.API.Models.Users;

namespace SiteYonetimSistemi.API.Models.UserApartments.DTOs
{
    public class UserApartmentDto
    {
        public Guid UserId { get; set; } = default!;
        public int ApartmentId { get; set; } = default!;
    }
}
