namespace HotelBookingApi.Models.Requests
{
    public class RoomBaseRequest
    {
        public Guid HotelId { get; set; }
        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public decimal PricePerNight { get; set; }

        public int Capacity { get; set; }

        public string? Amenities { get; set; }

        public int? TotalRooms { get; set; }

        public int? AvailableRooms { get; set; }
        public List<string> ImageUrls { get; set; }
    }
}
