using SiteYonetimSistemi.API.Models.Apartments;
using SiteYonetimSistemi.API.Models.Enums;
using SiteYonetimSistemi.API.Models.Users;
using System.ComponentModel.DataAnnotations.Schema;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SiteYonetimSistemi.API.Models.Payments
{
    public class Payment
    {
        public int Id { get; set; }
        public int Month { get; set; }
        public decimal Amount { get; set; }     
        public PaymentType PaymentType { get; set; } 
        public PaymentFor PaymentFor { get; set; } 
        public DateTime PaymentDate { get; set; }

        // One-to-many relationship with an apartment
        public int ApartmentId { get; set; }
        public Apartment Apartment { get; set; }
    }
}
