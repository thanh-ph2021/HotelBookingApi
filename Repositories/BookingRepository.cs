using Azure.Core;
using Dapper;
using HotelBookingApi.Common;
using HotelBookingApi.Data;
using HotelBookingApi.DTOs;
using HotelBookingApi.Models;
using HotelBookingApi.Models.Requests;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace HotelBookingApi.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly HotelBookingDbContext _context;
        private readonly IConfiguration _configuration;
        public BookingRepository(HotelBookingDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        public async Task<Result<bool>> CancelBookingAsync(Guid id)
        {
            var booking = await _context.Bookings.FindAsync(id);

            if (booking == null)
            {
                return Result<bool>.Failure("Booking not found.");
            }

            if (booking.StatusId == Guid.Parse("84978444-6B21-4D46-99C2-1877B00797F1"))
            {
                return Result<bool>.Failure("Booking has already been canceled.");
            }

            booking.StatusId = Guid.Parse("84978444-6B21-4D46-99C2-1877B00797F1"); // Canceled
            await _context.SaveChangesAsync();

            return Result<bool>.Success(true);
        }

        public async Task<Result<bool>> CreateBookingAsync(BookingRequest request)
        {
            var room = await _context.Rooms.FindAsync(request.RoomId);
            if (room == null)
            {
                return Result<bool>.Failure("Booking not found.");
            }

            if (request.CheckInDate >= request.CheckOutDate)
            {
                return Result<bool>.Failure("Ngày check-in phải trước ngày check-out.");
            }

            var booking = new Booking
            {
                Id = Guid.NewGuid(),
                RoomId = request.RoomId,
                UserId = request.UserId,
                StatusId = Guid.Parse("90B30E6C-E771-41B9-BC73-76A823092E7A"), // Pending
                CheckInDate = request.CheckInDate,
                CheckOutDate = request.CheckOutDate,
                TotalPrice = request.TotalPrice,
                CreatedAt = DateTime.UtcNow
            };

            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();

            return Result<bool>.Success(true);
        }

        public async Task<BookingDto> GetBookingDetailAsync(Guid id)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@BookingId", id, DbType.Guid);

                var result = await connection.QueryFirstOrDefaultAsync<BookingDto>(
                    "BP0001",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );

                return result;
            }
        }

        public async Task<IEnumerable<BookingDto>> FilterBookings(FilterBookingRequest request)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@UserId", request.UserId, DbType.Guid);
                parameters.Add("@PageIndex", request.PageIndex, DbType.Int32);
                parameters.Add("@PageSize", request.PageSize, DbType.Int32);

                var results = await connection.QueryAsync<BookingDto>(
                    "BP0002",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );

                return results.ToList();
            }
        }
    }
}
