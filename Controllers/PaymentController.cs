using HotelBookingApi.Extentions;
using HotelBookingApi.Models.Requests;
using HotelBookingApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingApi.Controllers
{
    [Route("api/payments")]
    [ApiController]
    [Authorize]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }
        [HttpPost]
        public async Task<IActionResult> ProcessPayment(PaymentRequest request)
        {
            var userId = User.GetUserId();
            request.UserId = userId;

            var result = await _paymentService.ProcessPaymentAsync(request);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result);
        }

        [HttpGet("{bookingId}")]
        public async Task<IActionResult> GetPaymentStatus(Guid bookingId)
        {
            var status = await _paymentService.GetPaymentStatusAsync(bookingId);

            if (status == null)
                return NotFound("Booking not found");

            return Ok(status);
        }
    }
}
