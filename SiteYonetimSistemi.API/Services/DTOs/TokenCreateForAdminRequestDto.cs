namespace SiteYonetimSistemi.API.Services.DTOs
{
    public class TokenCreateForAdminRequestDto
    {
        public string UserName { get; set; } = default!;
        public string Password { get; set; } = default!;
    }
}
