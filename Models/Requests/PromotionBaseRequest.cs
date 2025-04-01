namespace HotelBookingApi.Models.Requests
{
    public class PromotionBaseRequest
    {
        public Guid HotelId { get; set; }

        public decimal DiscountPercentage { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
