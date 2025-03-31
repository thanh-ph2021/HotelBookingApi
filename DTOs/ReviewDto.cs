namespace HotelBookingApi.DTOs
{
    public class ReviewDto
    {
        public Guid UserId { get; set; }

        public string UserName { get; set; } = null!;

        public string UserCountry { get; set; } = null!;

        public Guid HotelId { get; set; }

        public string RoomType { get; set; } = null!;

        public int StayDuration { get; set; }

        public DateTime StayMonth { get; set; }

        public decimal Rating { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }
    }
}
