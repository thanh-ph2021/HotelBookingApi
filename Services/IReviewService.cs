using HotelBookingApi.Common;
using HotelBookingApi.DTOs;
using HotelBookingApi.Models.Requests;

namespace HotelBookingApi.Services
{
    public interface IReviewService
    {
        public Task<Result<bool>> AddReviewAsync(ReviewRequest request);
        public Task<Result<bool>> DeleteReviewAsync(Guid id, string userRole, Guid userId);
        public Task<IEnumerable<ReviewDto>> GetReviewByHotelIdAsync(FilterReviewRequest request);
    }
}
