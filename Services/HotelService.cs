using Azure.Core;
using HotelBookingApi.Common;
using HotelBookingApi.DTOs;
using HotelBookingApi.Models;
using HotelBookingApi.Models.Requests;
using HotelBookingApi.Repositories;

namespace HotelBookingApi.Services
{
    public class HotelService : IHotelService
    {
        private readonly IHotelRepository _repository;

        public HotelService(IHotelRepository repository)
        {
            _repository = repository;
        }

        public async Task<HotelResponseDto> CreateHotelAsync(CreateHotelRequest request)
        {
            return await _repository.CreateHotelAsync(request);
        }

        public async Task<Result<bool>> DeleteHotelAsync(Guid id)
        {
            return await _repository.DeleteHotelAsync(id);
        }

        public async Task<IEnumerable<HotelDto>> FilterHotelsAsync(FilterHotelRequest request)
        {
            return await _repository.FilterHotelsAsync(request);
        }

        public async Task<HotelDetailResponse> GetHotelByIdAsync(Guid id)
        {
            return await _repository.GetHotelByIdAsync(id);
        }

        public async Task<HotelResponseDto> UpdateHotelAsync(UpdateHotelRequest request)
        {
            return await _repository.UpdateHotelAsync(request);
        }
    }
}
