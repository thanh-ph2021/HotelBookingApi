using Dapper;
using HotelBookingApi.Data;
using HotelBookingApi.DTOs;
using HotelBookingApi.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace HotelBookingApi.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly HotelBookingDbContext _context;
        private readonly IConfiguration _configuration;

        public UserRepository(HotelBookingDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<UserProfileDto?> GetUserByIdAsync(Guid userId)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@UserId", userId, DbType.Guid);
                parameters.Add("@Email", null, DbType.String);

                var user = await connection.QueryFirstOrDefaultAsync<UserProfileDto>(
                    "UP0001",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );

                return user;
            }
        }

        public async Task<bool> UpdateUserProfileAsync(Guid userId, UserProfileUpdateDto userProfileUpdate)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@UserId", userId, DbType.Guid);
                parameters.Add("@FullName", userProfileUpdate.FullName, DbType.String);
                parameters.Add("@Phone", userProfileUpdate.Phone, DbType.String);

                var affectedRows = await connection.ExecuteAsync(
                    "UP0002",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );

                return affectedRows > 0;
            }
        }

        public async Task<bool> UpdatePasswordAsync(Guid userId, string newPasswordHash)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null) return false;

            user.PasswordHash = newPasswordHash;
            _context.Users.Update(user);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
