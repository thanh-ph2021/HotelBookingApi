using Azure.Core;
using HotelBookingApi.Common;
using HotelBookingApi.DTOs;
using HotelBookingApi.Models.Requests;
using HotelBookingApi.Repositories;

namespace HotelBookingApi.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _repository;

        public BookingService(IBookingRepository repository)
        {
            _repository = repository;
        }
        public async Task<Result<bool>> CancelBookingAsync(Guid id)
        {
            return await _repository.CancelBookingAsync(id);
        }

        public async Task<Result<bool>> CreateBookingAsync(BookingRequest request)
        {
            return await _repository.CreateBookingAsync(request);
        }

        public async Task<IEnumerable<BookingDto>> FilterBookings(FilterBookingRequest request)
        {
            return await _repository.FilterBookings(request);
        }

        public async Task<BookingDto> GetBookingDetailAsync(Guid id)
        {
            return await _repository.GetBookingDetailAsync(id);
        }
    }
}
