using SiteYonetimSistemi.API.Models.Apartments;
using SiteYonetimSistemi.API.Models.Enums;

namespace SiteYonetimSistemi.API.Models.Payments.DTOs
{
    public class PaymentDto
    {
        public int Id { get; set; }
        public int Month { get; set; }
        public decimal Amount { get; set; }
        public PaymentType PaymentType { get; set; }
        public PaymentFor PaymentFor { get; set; }
        public DateTime PaymentDate { get; set; }
    }
}
