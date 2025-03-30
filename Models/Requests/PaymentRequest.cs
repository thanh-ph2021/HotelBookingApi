namespace HotelBookingApi.Models.Requests
{
    public class PaymentRequest
    {
        public Guid BookingId { get; set; }
        public Guid UserId { get; set; }
        public decimal Amount { get; set; }
        public Guid PaymentMethodId { get; set; }
    }
}
