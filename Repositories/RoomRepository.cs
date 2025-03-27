using Dapper;
using HotelBookingApi.Common;
using HotelBookingApi.Data;
using HotelBookingApi.DTOs;
using HotelBookingApi.Models;
using HotelBookingApi.Models.Requests;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace HotelBookingApi.Repositories
{
    public class RoomRepository : IRoomRepository
    {
        private readonly HotelBookingDbContext _context;
        private readonly IConfiguration _configuration;

        public RoomRepository(HotelBookingDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<IEnumerable<RoomDto>> GetRoomsAsync(Guid hotelId)
        {
            return await _context.Rooms
                .Where(r => r.HotelId == hotelId)
                .Select(r => new RoomDto
                {
                    Id = r.Id,
                    Name = r.Name,
                    Description = r.Description,
                    PricePerNight = r.PricePerNight,
                    Capacity = r.Capacity,
                    Amenities = r.Amenities,
                    AvailableRooms = r.AvailableRooms,
                    TotalRooms = r.TotalRooms
                })
                .ToListAsync();
        }

        public async Task<Result<bool>> DeleteRoomAsync(Guid id)
        {
            var room = await _context.Rooms.FindAsync(id);
            if (room == null)
            {
                return Result<bool>.Failure("Room not found.");
            }

            _context.Rooms.Remove(room);
            await _context.SaveChangesAsync();
            return Result<bool>.Success(true);
        }

        public async Task<RoomDetailResponse> GetRoomByIdAsync(Guid id)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@RoomId", id, DbType.Guid);

                var result = await connection.QueryFirstOrDefaultAsync<RoomDetailResponse>(
                    "RP0001",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );

                if (result != null && !string.IsNullOrEmpty(result.ImageUrls))
                {
                    result.ImageUrlList = result.ImageUrls.Split(',').ToList();
                }

                return result;
            }
        }

        public async Task<Result<bool>> CreateHotelAsync(CreateRoomRequest request)
        {
            var room = new Room
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Amenities = request.Amenities,
                AvailableRooms = request.AvailableRooms,
                Description = request.Description,
                Capacity = request.Capacity,
                PricePerNight = request.PricePerNight,
                HotelId = request.HotelId,
                TotalRooms = request.TotalRooms,
            };
            _context.Rooms.Add(room);

            if (request.ImageUrls != null && request.ImageUrls.Count > 0)
            {
                foreach (var url in request.ImageUrls)
                {
                    if (!string.IsNullOrEmpty(url))
                    {
                        _context.RoomImages.Add(new RoomImage
                        {
                            Id = Guid.NewGuid(),
                            RoomId = room.Id,
                            ImageUrl = url,
                        });
                    }
                }
            }

            await _context.SaveChangesAsync();

            return Result<bool>.Success(true);
        }

        public async Task<Result<bool>> UpdateHotelAsync(UpdateRoomRequest request)
        {
            var room = await _context.Rooms.FindAsync(request.Id);
            if (room == null)
            {
                return Result<bool>.Failure("Room not found");
            }

            room.Name = request.Name;
            room.Description = request.Description;
            room.PricePerNight = request.PricePerNight;
            room.Capacity = request.Capacity;
            room.Amenities = request.Amenities;
            room.AvailableRooms = request.AvailableRooms;
            room.TotalRooms = request.TotalRooms;

            await _context.SaveChangesAsync();

            return Result<bool>.Success(true);
        }
    }
}
