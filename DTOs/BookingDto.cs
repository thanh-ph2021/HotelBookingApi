namespace HotelBookingApi.DTOs
{
    public class BookingDto
    {
        public Guid Id { get; set; }
        public Guid RoomId { get; set; }
        public string RoomName { get; set; }
        public string StatusName { get; set; }
        public Guid StatusId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public decimal TotalPrice { get; set; }
        public string CreatedUserName { get; set; }
        public Guid CreatedUserId { get; set; }
    }
}
