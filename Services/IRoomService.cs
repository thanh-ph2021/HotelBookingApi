using HotelBookingApi.Common;
using HotelBookingApi.DTOs;
using HotelBookingApi.Models.Requests;

namespace HotelBookingApi.Services
{
    public interface IRoomService
    {
        Task<IEnumerable<RoomDto>> GetRoomsAsync(Guid hotelId);
        Task<Result<bool>> DeleteRoomAsync(Guid id);
        Task<RoomDetailResponse> GetRoomByIdAsync(Guid id);
        Task<Result<bool>> CreateHotelAsync(CreateRoomRequest request);
        Task<Result<bool>> UpdateHotelAsync(UpdateRoomRequest request);
    }
}
