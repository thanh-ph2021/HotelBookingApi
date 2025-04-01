using HotelBookingApi.Models;
using HotelBookingApi.Models.Requests;
using HotelBookingApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingApi.Controllers
{
    [Route("api/promotions")]
    [ApiController]
    [Authorize]
    public class PromotionController : ControllerBase
    {
        private readonly IPromotionService _promotionService;
        public PromotionController(IPromotionService promotionService)
        {
            _promotionService = promotionService;
        }

        [HttpPost("filter-promotions")]
        public async Task<IActionResult> FilterPromotion([FromBody] FilterPromotionRequest request)
        {
            var promotions = await _promotionService.FilterPromotion(request);
            return Ok(promotions);
        }

        [HttpPost("create-promotion")]
        public async Task<IActionResult> CreatePromotion([FromBody] CreatePromotionRequest request)
        {
            var result = await _promotionService.CreatePromotion(request);
            return Ok(result);
        }

        [HttpPost("update-promotion")]
        public async Task<IActionResult> UpdatePromotion([FromBody] UpdatePromotionRequest request)
        {
            return Ok(await _promotionService.UpdatePromotion(request));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePromotion(Guid id)
        {
            var result = await _promotionService.DeletePromotion(id);

            return Ok(result);
        }
    }
}
