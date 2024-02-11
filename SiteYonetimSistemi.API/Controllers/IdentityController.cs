using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SiteYonetimSistemi.API.Models.Users;
using SiteYonetimSistemi.API.Services;
using SiteYonetimSistemi.API.Services.DTOs;

namespace SiteYonetimSistemi.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class IdentityController(IdentityService identityService,TokenService tokenService) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateTokenForUser(TokenCreateForUserRequestDto request)
        {
            var response = await tokenService.CreateTokenForUser(request);
            if (response.AnyError)
            {
                return BadRequest(response);
            }
            return Created("", response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTokenForAdmin(TokenCreateForAdminRequestDto request)
        {
            var response = await tokenService.CreateTokenForAdmin(request);
            if (response.AnyError)
            {
                return BadRequest(response);
            }
            return Created("", response);
        }
    }
}
