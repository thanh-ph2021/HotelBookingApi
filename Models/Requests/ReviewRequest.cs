namespace HotelBookingApi.Models.Requests
{
    public class ReviewRequest
    {
        public Guid UserId { get; set; }

        public Guid HotelId { get; set; }

        public string RoomType { get; set; } = null!;

        public int StayDuration { get; set; }

        public DateOnly StayMonth { get; set; }

        public decimal Rating { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }
    }
}
