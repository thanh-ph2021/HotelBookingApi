using HotelBookingApi.Common;
using HotelBookingApi.DTOs;
using HotelBookingApi.Models;
using HotelBookingApi.Models.Requests;

namespace HotelBookingApi.Repositories
{
    public interface IHotelRepository
    {
        Task<IEnumerable<HotelDto>> FilterHotelsAsync(FilterHotelRequest request);
        Task<HotelResponseDto> CreateHotelAsync(CreateHotelRequest request);
        Task<HotelResponseDto> UpdateHotelAsync(UpdateHotelRequest request);
        Task<Result<bool>> DeleteHotelAsync(Guid id);
        Task<HotelDetailResponse> GetHotelByIdAsync(Guid id);
    }
}
