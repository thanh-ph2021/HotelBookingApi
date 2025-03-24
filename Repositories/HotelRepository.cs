using Azure.Core;
using Dapper;
using HotelBookingApi.Common;
using HotelBookingApi.Data;
using HotelBookingApi.DTOs;
using HotelBookingApi.Models;
using HotelBookingApi.Models.Requests;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace HotelBookingApi.Repositories
{
    public class HotelRepository : IHotelRepository
    {
        private readonly HotelBookingDbContext _context;
        private readonly IConfiguration _configuration;

        public HotelRepository(HotelBookingDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<HotelResponseDto> CreateHotelAsync(CreateHotelRequest request)
        {
            var hotel = new Hotel
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Description = request.Description,
                Address = request.Address,
                City = request.City,
                Country = request.Country,
                OwnerId = request.OwnerId
            };
            _context.Hotels.Add(hotel);

            if (!string.IsNullOrEmpty(request.BannerImage))
            {
                _context.HotelImages.Add(new HotelImage
                {
                    Id = Guid.NewGuid(),
                    HotelId = hotel.Id,
                    ImageUrl = request.BannerImage,
                    IsBanner = true
                });
            }

            if (request.ImageUrls != null && request.ImageUrls.Count > 0)
            {
                foreach (var url in request.ImageUrls)
                {
                    if (!string.IsNullOrEmpty(url))
                    {
                        _context.HotelImages.Add(new HotelImage
                        {
                            Id = Guid.NewGuid(),
                            HotelId = hotel.Id,
                            ImageUrl = url,
                            IsBanner = false
                        });
                    }
                }
            }

            await _context.SaveChangesAsync();

            return new HotelResponseDto
            {
                Id = hotel.Id,
                Name = hotel.Name,
                BannerImage = request.BannerImage
            };
        }

        public async Task<Result<bool>> DeleteHotelAsync(Guid id)
        {
            var hotel = await _context.Hotels.FindAsync(id);
            if (hotel == null)
            {
                return Result<bool>.Failure("User not found.");
            }
                
            _context.Hotels.Remove(hotel);
            await _context.SaveChangesAsync();
            return Result<bool>.Success(true);
        }

        public async Task<IEnumerable<HotelDto>> FilterHotelsAsync(FilterHotelRequest request)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@City", request.City, DbType.String);
                parameters.Add("@Country", request.Country, DbType.String);
                parameters.Add("@Rating", request.Rating, DbType.String);
                parameters.Add("@PageIndex", request.PageSize, DbType.Int32);
                parameters.Add("@PageSize", request.PageIndex, DbType.Int32);

                var results = await connection.QueryAsync<HotelDto>(
                    "HP0001",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );

                return results.ToList();
            }
        }

        public async Task<HotelDetailResponse> GetHotelByIdAsync(Guid id)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@HotelId", id, DbType.Guid);

                var result = await connection.QueryFirstOrDefaultAsync<HotelDetailResponse>(
                    "HP0002",
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

        public async Task<HotelResponseDto> UpdateHotelAsync(UpdateHotelRequest request)
        {
            var hotel = await _context.Hotels.FindAsync(request.Id);
            if (hotel == null)
            {
                throw new Exception("Hotel not found");
            }

            hotel.Name = request.Name;
            hotel.Description = request.Description;
            hotel.Address = request.Address;
            hotel.City = request.City;
            hotel.Country = request.Country;

            await _context.SaveChangesAsync();

            return new HotelResponseDto
            {
                Id = hotel.Id,
                Name = hotel.Name,
                BannerImage = request.BannerImage
            };
        }
    }
}
