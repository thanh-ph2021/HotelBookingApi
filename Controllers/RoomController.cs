using Azure.Core;
using HotelBookingApi.DTOs;
using HotelBookingApi.Models;
using HotelBookingApi.Models.Requests;
using HotelBookingApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HotelBookingApi.Controllers
{
    [Route("api/room")]
    [ApiController]
    [Authorize]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _roomService;
        public RoomController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        [HttpGet("hotelId={id}")]
        public async Task<IActionResult> GetRoomsByHotelId(Guid id)
        {
            var result = await _roomService.GetRoomsAsync(id);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDetailRoom(Guid id)
        {
            var room = await _roomService.GetRoomByIdAsync(id);
            if (room == null) return NotFound();
            return Ok(room);
        }

        [HttpPost("create-room")]
        public async Task<IActionResult> CreateRoom(CreateRoomRequest request)
        {
            if (request == null)
            {
                return BadRequest("Invalid Data");
            }

            return Ok(await _roomService.CreateHotelAsync(request));
        }

        [HttpPost("update-room")]
        public async Task<IActionResult> UpdateRoom(UpdateRoomRequest request)
        {
            if (request == null)
            {
                return BadRequest("Invalid Data");
            }

            return Ok(await _roomService.UpdateHotelAsync(request));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoom(Guid id)
        {
            var result = await _roomService.DeleteRoomAsync(id);

            if (!result.IsSuccess)
                return BadRequest(result.Error);

            return Ok("Room deleted successfully.");
        }
    }
}
