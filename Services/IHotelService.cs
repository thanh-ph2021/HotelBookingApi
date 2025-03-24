using HotelBookingApi.Common;
using HotelBookingApi.DTOs;
using HotelBookingApi.Models.Requests;
using System.Collections;

namespace HotelBookingApi.Services
{
    public interface IHotelService
    {
        Task<IEnumerable<HotelDto>> FilterHotelsAsync(FilterHotelRequest request);
        Task<HotelResponseDto> CreateHotelAsync(CreateHotelRequest request);
        Task<HotelResponseDto> UpdateHotelAsync(UpdateHotelRequest request);
        Task<Result<bool>> DeleteHotelAsync(Guid id);
        Task<HotelDetailResponse> GetHotelByIdAsync(Guid id);
    }
}
