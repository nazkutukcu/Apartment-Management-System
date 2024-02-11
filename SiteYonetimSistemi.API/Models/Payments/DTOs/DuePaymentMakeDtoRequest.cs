using SiteYonetimSistemi.API.Models.Enums;
using SiteYonetimSistemi.API.Models.Users;
using System.ComponentModel.DataAnnotations;

namespace SiteYonetimSistemi.API.Models.Payments.DTOs
{
    public class DuePaymentMakeDtoRequest
    {
        [Required(ErrorMessage = "Daire numarası gereklidir.")]
        public int ApartmentId { get; set; }

        [Required(ErrorMessage = "Ay bilgisi gereklidir.")]
        [Range(1, 12, ErrorMessage = "Ay 1 ile 12 arasında olmalıdır.")]
        public int Month { get; set; }

        [Required(ErrorMessage = "Ödeme tipi gereklidir.")]
        [EnumDataType(typeof(PaymentType), ErrorMessage = "Geçerli bir ödeme tipi giriniz.")]
        public PaymentType PaymentType { get; set; }        
    }
}
