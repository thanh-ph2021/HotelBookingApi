using HotelBookingApi.Common;
using HotelBookingApi.DTOs;
using HotelBookingApi.Models.Requests;

namespace HotelBookingApi.Repositories
{
    public interface ISupportRequestRepository
    {
        Task<Result<bool>> AddSupportRequestAsync(SRRequest request);
        Task<SupportRequestDto> GetSupportStatusAsync(Guid id);
    }
}
