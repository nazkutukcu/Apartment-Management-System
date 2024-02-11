using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SiteYonetimSistemi.API.Models.Payments;
using SiteYonetimSistemi.API.Models.Payments.DTOs;

namespace SiteYonetimSistemi.API.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PaymentController(IPaymentService paymentService) : ControllerBase
    {
        [Authorize(Roles = "User")]
        [HttpPost]
        public IActionResult MakePaymentForMonthlyDue(DuePaymentMakeDtoRequest request)
        {
            var result = paymentService.MakePaymentForMonthlyDue(request);
            return Created("", result);
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        public IActionResult MakePaymentForMonthlyBill(BillPaymentMakeDtoRequest request)
        {
            var result = paymentService.MakePaymentForMonthlyBill(request);
            return Created("", result);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{apartmentId}")]
        public IActionResult GetPaymentsForApartment(int apartmentId)
        {
            var result = paymentService.GetPaymentsForApartment(apartmentId);
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult GetApartmentsWithRegularPayments()
        {
            var response = paymentService.GetApartmentsWithRegularPayments();
            if (response.AnyError)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }


    }
}
