using HotelBookingApi.Common;
using HotelBookingApi.DTOs;
using HotelBookingApi.Models.Requests;

namespace HotelBookingApi.Services
{
    public interface IPromotionService
    {
        public Task<IEnumerable<PromotionDto>> FilterPromotion(FilterPromotionRequest request);
        public Task<Result<bool>> CreatePromotion(CreatePromotionRequest request);
        public Task<Result<bool>> UpdatePromotion(UpdatePromotionRequest request);
        public Task<Result<bool>> DeletePromotion(Guid id);
    }
}
