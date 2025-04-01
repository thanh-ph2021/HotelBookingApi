using Azure.Core;
using HotelBookingApi.Extentions;
using HotelBookingApi.Models.Requests;
using HotelBookingApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingApi.Controllers
{
    [Route("api/support")]
    [ApiController]
    [Authorize]
    public class SupportRequestController : ControllerBase
    {
        private readonly ISupportRequestService _supportRequestService;
        public SupportRequestController(ISupportRequestService supportRequestService)
        {
            _supportRequestService = supportRequestService;
        }

        [HttpPost("send-request")]
        public async Task<IActionResult> SendRequest(SRRequest request)
        {
            var userId = User.GetUserId();
            request.UserId = userId;

            var result = await _supportRequestService.AddSupportRequestAsync(request);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSupportStatusAsync(Guid id)
        {
            var result = await _supportRequestService.GetSupportStatusAsync(id);

            return Ok(result);
        }
    }
}
