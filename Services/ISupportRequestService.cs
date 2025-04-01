using HotelBookingApi.Common;
using HotelBookingApi.DTOs;
using HotelBookingApi.Models.Requests;

namespace HotelBookingApi.Services
{
    public interface ISupportRequestService
    {
        Task<Result<bool>> AddSupportRequestAsync(SRRequest request);
        Task<SupportRequestDto> GetSupportStatusAsync(Guid id);
    }
}
