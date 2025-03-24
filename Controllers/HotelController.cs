using HotelBookingApi.Common;
using HotelBookingApi.DTOs;
using HotelBookingApi.Models.Requests;
using HotelBookingApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HotelBookingApi.Controllers
{
    [Route("api/hotel")]
    [ApiController]
    [Authorize]
    public class HotelController : ControllerBase
    {
        private IHotelService _hotelService;

        public HotelController(IHotelService hotelService)
        {
            _hotelService = hotelService;
        }

        [HttpPost("filter")]
        public async Task<IActionResult> FilterHotelsAsync([FromBody] FilterHotelRequest request)
        {
            var result = await _hotelService.FilterHotelsAsync(request);
            return Ok(result);
        }

        [HttpPost("create-hotel")]
        public async Task<IActionResult> CreateHotel([FromBody]CreateHotelRequest request)
        {
            if(request == null)
            {
                return BadRequest("Invalid Data");
            }

            var createdHotel = await _hotelService.CreateHotelAsync(request);
            return CreatedAtAction(nameof(GetHotelById), new { id = createdHotel.Id }, createdHotel);
        }

        [HttpPost("update-hotel")]
        public async Task<IActionResult> UpdateHotel([FromBody] UpdateHotelRequest request)
        {
            if (request == null)
                return BadRequest("Invalid data");

            try
            {
                var updatedHotel = await _hotelService.UpdateHotelAsync(request);
                return Ok(updatedHotel);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetHotelById(Guid id)
        {
            var hotel = await _hotelService.GetHotelByIdAsync(id);
            if (hotel == null) return NotFound();
            return Ok(hotel);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHotel(Guid id)
        {
            var result = await _hotelService.DeleteHotelAsync(id);

            if (!result.IsSuccess)
                return BadRequest(result.Error);

            return Ok("Hotel deleted successfully.");
        }
    }
}
