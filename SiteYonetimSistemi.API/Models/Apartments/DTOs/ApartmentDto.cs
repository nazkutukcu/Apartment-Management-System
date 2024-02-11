using SiteYonetimSistemi.API.Models.Enums;
using SiteYonetimSistemi.API.Models.Users;

namespace SiteYonetimSistemi.API.Models.Apartments.DTOs
{
    public class ApartmentDto
    {
        public int ApartmentNo { get; set; }  
        public Block Block { get; set; } 
        public ApartmentType Type { get; set; } 
        public int Floor { get; set; } 
        public ResidenceStatus ResidenceStatus { get; set; } 
        public bool IsFull { get; set; }

    }
}
