namespace HotelBookingApi.DTOs
{
    public class PaymentStatusDto
    {
        public Guid BookingId { get; set; }
        public string StatusName { get; set; }
        public Guid StatusId { get; set; }
        public DateTime? PaidAt { get; set; }
        public decimal Amount { get; set; }
        public string PaymentMethodsName { get; set; }

    }
}
