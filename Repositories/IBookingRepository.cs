using HotelBookingApi.Common;
using HotelBookingApi.DTOs;
using HotelBookingApi.Models.Requests;

namespace HotelBookingApi.Repositories
{
    public interface IBookingRepository
    {
        Task<Result<bool>> CreateBookingAsync(BookingRequest request);
        Task<IEnumerable<BookingDto>> FilterBookings(FilterBookingRequest request);
        Task<BookingDto> GetBookingDetailAsync(Guid id);
        Task<Result<bool>> CancelBookingAsync(Guid id);
    }
}
