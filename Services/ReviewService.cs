using Azure.Core;
using HotelBookingApi.Common;
using HotelBookingApi.DTOs;
using HotelBookingApi.Models.Requests;
using HotelBookingApi.Repositories;

namespace HotelBookingApi.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _repository;
        public ReviewService(IReviewRepository repository) 
        {
            _repository = repository;
        }
        public async Task<Result<bool>> AddReviewAsync(ReviewRequest request)
        {
            return await _repository.AddReviewAsync(request);
        }

        public async Task<Result<bool>> DeleteReviewAsync(Guid id, string userRole, Guid userId)
        {
            var createdUserId= await _repository.GetCreatedUserIdAsync(id);
            if (userRole != "Admin" && createdUserId.Value != userId)
            {
                return Result<bool>.Failure("You are not authorized to delete this review.");
            }
            return await _repository.DeleteReviewAsync(id);
        }

        public async Task<IEnumerable<ReviewDto>> GetReviewByHotelIdAsync(FilterReviewRequest request)
        {
            return await _repository.GetReviewByHotelIdAsync(request);
        }
    }
}
