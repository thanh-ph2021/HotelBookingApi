using HotelBookingApi.DTOs;
using HotelBookingApi.Models.Requests;

namespace HotelBookingApi.Repositories
{
    public interface IPaymentRepository
    {
        Task<PaymentResult> ProcessPaymentAsync(PaymentRequest request);
        Task<PaymentStatusDto> GetPaymentStatusAsync(Guid bookingId);
    }
}
