using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SiteYonetimSistemi.API.Models.Apartments;
using SiteYonetimSistemi.API.Models.Apartments.DTOs;
using SiteYonetimSistemi.API.Models.Users;
using SiteYonetimSistemi.API.Models.Users.DTOs;

namespace SiteYonetimSistemi.API.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ApartmentController(IApartmentService apartmentService) : ControllerBase
    {
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(apartmentService.GetAll());
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Add(ApartmentSaveDtoRequest request)
        {
            var result = apartmentService.AddApartment(request);
            return Created("", result);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{month}")]
        public IActionResult GetAllApartmentsUnpaidBillsAndDuesForMonth(int month)
        {
            var result = apartmentService.GetAllApartmentsUnpaidBillsAndDuesForMonth(month);
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{apartmentId}/{month}")]
        public IActionResult GetUnpaidBillsAndDuesForMonth( int apartmentId, int month)
        {
            var result = apartmentService.GetUnpaidBillsAndDuesForMonth(apartmentId, month);
            return Ok(result);
        }
    
        [Authorize(Roles = "User")]
        [HttpGet("{apartmentId}/{month}")]
        public IActionResult GetAllBillsAndDuesForMonth(int apartmentId, int month)
        {
            var result = apartmentService.GetAllBillsAndDuesForMonth(apartmentId, month);
            return Ok(result);
        }
    }
}
