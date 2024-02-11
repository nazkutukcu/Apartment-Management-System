using SiteYonetimSistemi.API.Models.BuildingMonthlyBills;
using SiteYonetimSistemi.API.Models.Enums;
using SiteYonetimSistemi.API.Models.MonthlyDues;
using SiteYonetimSistemi.API.Models.Payments;
using SiteYonetimSistemi.API.Models.UserApartments;
using SiteYonetimSistemi.API.Models.Users;
using System.Text.Json.Serialization;

namespace SiteYonetimSistemi.API.Models.Apartments
{
    public class Apartment
    {
        public int Id { get; set; }
        public int ApartmentNo { get; set; }      
        public Block Block { get; set; }     
        public ApartmentType Type { get; set; } 
        public int Floor { get; set; }    
        public ResidenceStatus ResidenceStatus { get; set; }
        public bool IsFull { get; set; } = false;

        //Relationships
        public UserApartment UserApartment { get; set; } 
        public List<MonthlyDue> MonthlyDues { get; set; }                                                       
        public List<MonthlyBill> MonthlyBills { get; set; } 
        public List<Payment> Payments { get; set; }
    }
}

