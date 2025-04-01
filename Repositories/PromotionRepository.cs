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
    public class PromotionRepository : IPromotionRepository
    {
        private readonly HotelBookingDbContext _context;
        private readonly IConfiguration _configuration;

        public PromotionRepository(HotelBookingDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        public async Task<Result<bool>> CreatePromotion(CreatePromotionRequest request)
        {
            var promotion = new Promotion
            {
                Id = Guid.NewGuid(),
                HotelId = request.HotelId,
                DiscountPercentage = request.DiscountPercentage,
                EndDate = request.EndDate,
                StartDate = request.StartDate,
                CreatedAt = DateTime.UtcNow,
            };

            _context.Promotions.Add(promotion);
            await _context.SaveChangesAsync();

            return Result<bool>.Success(true);
        }

        public async Task<Result<bool>> DeletePromotion(Guid id)
        {
            var promotion = await _context.Promotions.FindAsync(id);
            if (promotion == null)
            {
                return Result<bool>.Failure("Promotion not found.");
            }

            _context.Promotions.Remove(promotion);
            await _context.SaveChangesAsync();
            return Result<bool>.Success(true);
        }

        public async Task<IEnumerable<PromotionDto>> FilterPromotion(FilterPromotionRequest request)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@HotelId", request.HotelId, DbType.Guid);
                parameters.Add("@PageIndex", request.PageIndex, DbType.Int32);
                parameters.Add("@PageSize", request.PageSize, DbType.Int32);

                var results = await connection.QueryAsync<PromotionDto>(
                    "PROP0001",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );

                return results.ToList();
            }
        }

        public async Task<Result<bool>> UpdatePromotion(UpdatePromotionRequest request)
        {
            var promotion = await _context.Promotions.FindAsync(request.Id);
            if (promotion == null)
            {
                return Result<bool>.Failure("Promotion not found.");
            }

            promotion.StartDate = request.StartDate;
            promotion.EndDate = request.EndDate;
            promotion.DiscountPercentage = request.DiscountPercentage;

            await _context.SaveChangesAsync();
            return Result<bool>.Success(true);
        }
    }
}
