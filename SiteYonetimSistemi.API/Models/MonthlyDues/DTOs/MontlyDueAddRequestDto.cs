using SiteYonetimSistemi.API.Models.Apartments;
using System.ComponentModel.DataAnnotations;

namespace SiteYonetimSistemi.API.Models.MonthlyDues.DTOs
{
    public class MontlyDueAddRequestDto
    {
        public int ApartmentId { get; set; }

        [Required(ErrorMessage = "Amount alanı boş geçilemez.")]
        [Range(1, double.MaxValue, ErrorMessage = "Amount alanı 0'dan büyük bir değer olmalıdır.")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "Ay değeri zorunludur.")]
        [Range(1, 12, ErrorMessage = "Geçerli bir ay değeri giriniz (1-12).")]
        public int Month { get; set; } 
    }
}
