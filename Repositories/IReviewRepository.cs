using HotelBookingApi.Common;
using HotelBookingApi.DTOs;
using HotelBookingApi.Models.Requests;

namespace HotelBookingApi.Repositories
{
    public interface IReviewRepository
    {
        public Task<Result<bool>> AddReviewAsync(ReviewRequest request);
        public Task<Result<bool>> DeleteReviewAsync(Guid id);
        public Task<IEnumerable<ReviewDto>> GetReviewByHotelIdAsync(FilterReviewRequest request);
        public Task<Result<Guid>> GetCreatedUserIdAsync(Guid id);
    }
}
