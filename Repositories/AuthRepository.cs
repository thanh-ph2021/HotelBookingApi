using Azure;
using Dapper;
using HotelBookingApi.Data;
using HotelBookingApi.DTOs;
using HotelBookingApi.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Threading.Tasks;

namespace HotelBookingApi.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly HotelBookingDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthRepository(HotelBookingDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<User> CreateUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<UserProfileDto?> GetUserByEmailAsync(string email)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@UserId", null, DbType.Guid);
                parameters.Add("@Email", email, DbType.String);

                var user = await connection.QueryFirstOrDefaultAsync<UserProfileDto>(
                    "UP0001",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );

                return user;
            }
        }

        public async Task<UserProfileDto?> GetUserByFacebookIdAsync(string facebookId)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@UserId", null, DbType.Guid);
                parameters.Add("@Email", null, DbType.String);
                parameters.Add("@FacebookId", facebookId, DbType.String);

                var user = await connection.QueryFirstOrDefaultAsync<UserProfileDto>(
                    "UP0001",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );

                return user;
            }
        }
    }
}
