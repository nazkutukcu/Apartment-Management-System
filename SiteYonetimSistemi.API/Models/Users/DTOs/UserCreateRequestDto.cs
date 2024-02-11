using System.ComponentModel.DataAnnotations;

namespace SiteYonetimSistemi.API.Models.Users.DTOs
{
    public class UserCreateRequestDto
    {
        [Required(ErrorMessage = "İsim alanı zorunludur.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Soyisim alanı zorunludur.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Kimlik numarası alanı zorunludur.")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "Kimlik numarası 11 karakter olmalıdır.")]
        public string IdentityNumber { get; set; }

        [Required(ErrorMessage = "Email alanı zorunludur.")]
        [EmailAddress(ErrorMessage = "Geçerli bir email adresi giriniz.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Telefon numarası alanı zorunludur.")]
        [Phone(ErrorMessage = "Geçerli bir telefon numarası giriniz.")]
        public string PhoneNumber { get; set; }
    }
}
