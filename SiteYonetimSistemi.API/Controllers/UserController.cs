using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SiteYonetimSistemi.API.Models.Payments;
using SiteYonetimSistemi.API.Models.UserApartments;
using SiteYonetimSistemi.API.Models.UserApartments.DTOs;
using SiteYonetimSistemi.API.Models.Users;
using SiteYonetimSistemi.API.Models.Users.DTOs;
using SiteYonetimSistemi.API.Services;
using SiteYonetimSistemi.API.Services.DTOs;

namespace SiteYonetimSistemi.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IUserService userService) : ControllerBase
    {
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(userService.GetAll());
        }

        [Authorize(Roles = "Admin")] 
        [HttpPost]
        public async Task<IActionResult> CreateUser(UserCreateRequestDto request)
        {
            var response = await userService.CreateUser(request);
            if(response.AnyError)
            {
                return BadRequest(response);
            }
            return Created("", response);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult> Update(UserUpdateRequestDto request)
        {
            var response = await userService.Update(request);
            if (response.AnyError)
            {
                return BadRequest(response);
            }
            return Created("", response);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{userId}")]
        public async Task<IActionResult> Delete(Guid userId)
        {
            var response = await userService.Delete(userId);
            if (response.AnyError)
            {
                return BadRequest(response);
            }
            return NoContent();
        }

    }
}
