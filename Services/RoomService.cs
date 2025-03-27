using HotelBookingApi.Common;
using HotelBookingApi.DTOs;
using HotelBookingApi.Models;
using HotelBookingApi.Models.Requests;
using HotelBookingApi.Repositories;

namespace HotelBookingApi.Services
{
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _repository;

        public RoomService(IRoomRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<bool>> CreateHotelAsync(CreateRoomRequest request)
        {
            return await _repository.CreateHotelAsync(request);
        }

        public async Task<Result<bool>> DeleteRoomAsync(Guid id)
        {
            return await _repository.DeleteRoomAsync(id);
        }

        public async Task<RoomDetailResponse> GetRoomByIdAsync(Guid id)
        {
            return await _repository.GetRoomByIdAsync(id);
        }

        public async Task<IEnumerable<RoomDto>> GetRoomsAsync(Guid hotelId)
        {
            return await _repository.GetRoomsAsync(hotelId);
        }

        public async Task<Result<bool>> UpdateHotelAsync(UpdateRoomRequest request)
        {
            return await _repository.UpdateHotelAsync(request);
        }
    }
}
