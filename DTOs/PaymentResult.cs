namespace HotelBookingApi.DTOs
{
    public class PaymentResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public Guid BookingId { get; set; }
    }
}
