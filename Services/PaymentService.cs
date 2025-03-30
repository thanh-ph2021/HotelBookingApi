using HotelBookingApi.DTOs;
using HotelBookingApi.Models.Requests;
using HotelBookingApi.Repositories;

namespace HotelBookingApi.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _repository;
        public PaymentService(IPaymentRepository repository)
        {
            _repository = repository;
        }
        public async Task<PaymentStatusDto> GetPaymentStatusAsync(Guid bookingId)
        {
            return await _repository.GetPaymentStatusAsync(bookingId);
        }

        public async Task<PaymentResult> ProcessPaymentAsync(PaymentRequest request)
        {
            return await _repository.ProcessPaymentAsync(request);
        }
    }
}
