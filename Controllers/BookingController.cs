using Azure.Core;
using HotelBookingApi.Models.Requests;
using HotelBookingApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingApi.Controllers
{
    [Route("api/bookings")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;
        public BookingController(IBookingService service)
        {
            _bookingService = service;
        }

        [HttpPost]
        public async Task<IActionResult> Bookings([FromBody] BookingRequest request)
        {
            if (request == null)
            {
                return BadRequest("Invalid Data");
            }

            return Ok(await _bookingService.CreateBookingAsync(request));
        }

        [HttpPost("filter-bookings")]
        public async Task<IActionResult> FilterBookings([FromBody] FilterBookingRequest request)
        {

            var result = await _bookingService.FilterBookings(request);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookingDetail(Guid id)
        {
            var booking = await _bookingService.GetBookingDetailAsync(id);
            if (booking == null) return NotFound();
            return Ok(booking);
        }

        [HttpPost("{id}/cancel")]
        public async Task<IActionResult> CancelBooking(Guid id)
        {

            return Ok(await _bookingService.CancelBookingAsync(id));
        }
    }
}
