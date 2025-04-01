using Azure.Core;
using HotelBookingApi.Common;
using HotelBookingApi.DTOs;
using HotelBookingApi.Models.Requests;
using HotelBookingApi.Repositories;

namespace HotelBookingApi.Services
{
    public class PromotionService : IPromotionService
    {
        private readonly IPromotionRepository _repository;
        public PromotionService(IPromotionRepository repository)
        {
            _repository = repository;
        }
        public async Task<Result<bool>> CreatePromotion(CreatePromotionRequest request)
        {
            return await _repository.CreatePromotion(request);
        }

        public async Task<Result<bool>> DeletePromotion(Guid id)
        {
            return await _repository.DeletePromotion(id);
        }

        public async Task<IEnumerable<PromotionDto>> FilterPromotion(FilterPromotionRequest request)
        {
            return await _repository.FilterPromotion(request);
        }

        public async Task<Result<bool>> UpdatePromotion(UpdatePromotionRequest request)
        {
            return await _repository.UpdatePromotion(request);
        }
    }
}
