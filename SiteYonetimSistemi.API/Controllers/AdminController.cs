using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SiteYonetimSistemi.API.Models.Admin;
using SiteYonetimSistemi.API.Models.MonthlyBills.DTOs;
using SiteYonetimSistemi.API.Models.MonthlyDues.DTOs;
using SiteYonetimSistemi.API.Models.UserApartments;
using SiteYonetimSistemi.API.Models.UserApartments.DTOs;

namespace SiteYonetimSistemi.API.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AdminController(IAdminService adminService, IUserApartmentService userApartmentService) : ControllerBase
    {
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult AssignUserToApartment(UserApartmentDto request)
        {
            var result = userApartmentService.AssignUserToApartment(request);
            return Created("", result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult AssignMonthlyDuesToApartment(MontlyDueAddRequestDto request)
        {
            var result = adminService.AssignMonthlyDuesToApartment(request);
            return Created("", result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult AssignMonthlyDuesToAllApartments(MontlyDuesToAllAddRequestDto request)
        {
            var result = adminService.AssignMonthlyDuesToAll(request);
            return Created("", result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult AddMonthlyBillToAllApartments(MonthlyBillToAllAddRequestDto request)
        {
            var result = adminService.AddMonthlyBillToAllApartments(request);
            return Created("", result);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{apartmentId}/{month}")]
        public IActionResult GetMonthlyDebtForApartment(int apartmentId, int month)
        {
            var result = adminService.GetMonthlyDebtForApartment(apartmentId, month);
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{apartmentId}")]
        public IActionResult GetYearlyDebtForApartment(int apartmentId)
        {
            var result = adminService.GetYearlyDebtForApartment(apartmentId);
            return Ok(result);
        }

    }
}
