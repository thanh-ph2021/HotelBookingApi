namespace HotelBookingApi.DTOs
{
    public class PromotionDto
    {
        public Guid Id { get; set; }
        public Guid HotelId { get; set; }
        public decimal DiscountPercentage { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
