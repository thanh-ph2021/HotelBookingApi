using Azure.Core;
using Dapper;
using HotelBookingApi.Common;
using HotelBookingApi.Data;
using HotelBookingApi.DTOs;
using HotelBookingApi.Models;
using HotelBookingApi.Models.Requests;
using Microsoft.Data.SqlClient;
using System.Data;

namespace HotelBookingApi.Repositories
{
    public class SupportRequestRepository : ISupportRequestRepository
    {
        private readonly HotelBookingDbContext _context;
        private readonly IConfiguration _configuration;

        public SupportRequestRepository(HotelBookingDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<Result<bool>> AddSupportRequestAsync(SRRequest request)
        {
            var support = new SupportRequest
            {
                Id = Guid.NewGuid(),
                Message = request.Message,
                UserId = request.UserId,
                StatusId = Guid.Parse("1ECF5144-E9E9-45B2-80C4-F042355D5BE0"),
                Subject = request.Subject,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _context.SupportRequests.Add(support);
            await _context.SaveChangesAsync();

            return Result<bool>.Success(true);
        }

        public async Task<SupportRequestDto> GetSupportStatusAsync(Guid id)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@SRId", id, DbType.Guid);

                var result = await connection.QueryFirstOrDefaultAsync<SupportRequestDto>(
                    "SRP0001",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );

                return result;
            }
        }
    }
}
