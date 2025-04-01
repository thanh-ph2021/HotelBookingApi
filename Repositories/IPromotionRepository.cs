using HotelBookingApi.Common;
using HotelBookingApi.DTOs;
using HotelBookingApi.Models.Requests;

namespace HotelBookingApi.Repositories
{
    public interface IPromotionRepository
    {
        public Task<IEnumerable<PromotionDto>> FilterPromotion(FilterPromotionRequest request);
        public Task<Result<bool>> CreatePromotion(CreatePromotionRequest request);
        public Task<Result<bool>> UpdatePromotion(UpdatePromotionRequest request);
        public Task<Result<bool>> DeletePromotion(Guid id);
    }
}
