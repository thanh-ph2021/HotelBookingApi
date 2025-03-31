using Azure.Core;
using HotelBookingApi.Extentions;
using HotelBookingApi.Models.Requests;
using HotelBookingApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingApi.Controllers
{
    [Route("api/reviews")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;
        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpPost]
        public async Task<IActionResult> AddReview(ReviewRequest request)
        {
            var userId = User.GetUserId();
            request.UserId = userId;

            var result = await _reviewService.AddReviewAsync(request);

            return Ok(result);
        }

        [HttpPost("filter")]
        public async Task<IActionResult> GetReviewByHotelId([FromBody] FilterReviewRequest request)
        {
            var results = await _reviewService.GetReviewByHotelIdAsync(request);
            return Ok(results);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReview(Guid id)
        {
            var userRole = User.GetUserRole();
            var userId = User.GetUserId();

            var result = await _reviewService.DeleteReviewAsync(id, userRole, userId);

            return Ok(result);
        }
    }
}
