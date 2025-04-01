using Azure.Core;
using HotelBookingApi.Common;
using HotelBookingApi.DTOs;
using HotelBookingApi.Models.Requests;
using HotelBookingApi.Repositories;

namespace HotelBookingApi.Services
{
    public class SupportRequestService : ISupportRequestService
    {
        private readonly ISupportRequestRepository _repository;
        public SupportRequestService(ISupportRequestRepository repository)
        {
            _repository = repository;
        }
        public async Task<Result<bool>> AddSupportRequestAsync(SRRequest request)
        {
            return await _repository.AddSupportRequestAsync(request);
        }

        public async Task<SupportRequestDto> GetSupportStatusAsync(Guid id)
        {
            return await _repository.GetSupportStatusAsync(id);
        }
    }
}
