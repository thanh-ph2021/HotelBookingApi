using HotelBookingApi.DTOs;
using HotelBookingApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        private IHotelService _hotelService;

        public HotelController(IHotelService hotelService)
        {
            _hotelService = hotelService;
        }

        [HttpGet]
        public IActionResult GetHotels()
        {
            return Ok(_hotelService.GetHotels());
        }

        [HttpPost]
        public IActionResult CreateHotel([FromBody]HotelDto hotelDto)
        {
            if(hotelDto == null)
            {
                return BadRequest("Invalid Data");
            }

            _hotelService.CreateHotel(hotelDto);
            return Ok("Hotel created successfully");
        }
    }
}
