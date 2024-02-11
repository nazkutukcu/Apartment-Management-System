using SiteYonetimSistemi.API.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace SiteYonetimSistemi.API.Models.MonthlyBills.DTOs
{
    public class MonthlyBillToAllAddRequestDto
    {
        [Required(ErrorMessage = "Fatura türü zorunludur.")]
        [Range(1, 3, ErrorMessage = "Geçerli bir fatura türü seçiniz ( 1(Elektrik) - 2(Su) - 3 (Gaz) ).")]
        public BillType BillType { get; set; } 

        [Required(ErrorMessage = "Amount alanı boş geçilemez.")]
        [Range(1, double.MaxValue, ErrorMessage = "Amount alanı 0'dan büyük bir değer olmalıdır.")]
        public decimal Amount { get; set; } 

        [Required(ErrorMessage = "Ay değeri zorunludur.")]
        [Range(1, 12, ErrorMessage = "Geçerli bir ay değeri giriniz (1-12).")]
        public int Month { get; set; } 
    }
}
