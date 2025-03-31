using Azure.Core;
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
    public class ReviewRepository : IReviewRepository
    {
        private readonly HotelBookingDbContext _context;
        private readonly IConfiguration _configuration;

        public ReviewRepository(HotelBookingDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        public async Task<Result<bool>> AddReviewAsync(ReviewRequest request)
        {
            var review = new Review
            {
                Id = Guid.NewGuid(),
                HotelId = request.HotelId,
                UserId = request.UserId,
                Rating = request.Rating,
                Description = request.Description,
                Title = request.Title,
                StayDuration = request.StayDuration,
                StayMonth = request.StayMonth,
                RoomType = request.RoomType,
                CreatedAt = DateTime.UtcNow,
            };

            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();

            return Result<bool>.Success(true);
        }

        public async Task<Result<bool>> DeleteReviewAsync(Guid id)
        {
            var review = await _context.Reviews.FindAsync(id);
            if (review == null)
            {
                return Result<bool>.Failure("Review not found.");
            }

            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();
            return Result<bool>.Success(true);
        }

        public async Task<Result<Guid>> GetCreatedUserIdAsync(Guid id)
        {
            var review = await _context.Reviews.FindAsync(id);
            if (review == null)
            {
                return Result<Guid>.Failure("Review not found.");
            }

            return Result<Guid>.Success(review.UserId);
        }

        public async Task<IEnumerable<ReviewDto>> GetReviewByHotelIdAsync(FilterReviewRequest request)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@HotelId", request.HotelId, DbType.Guid);
                parameters.Add("@PageIndex", request.PageIndex, DbType.Int32);
                parameters.Add("@PageSize", request.PageSize, DbType.Int32);

                var results = await connection.QueryAsync<ReviewDto>(
                    "REVP0001",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );

                return results.ToList();
            }
        }
    }
}
