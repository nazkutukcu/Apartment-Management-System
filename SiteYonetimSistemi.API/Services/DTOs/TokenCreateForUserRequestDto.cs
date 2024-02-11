namespace SiteYonetimSistemi.API.Services.DTOs
{
    public class TokenCreateForUserRequestDto
    {
        public string IdentityNumber { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
    }
}
