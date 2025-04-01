namespace HotelBookingApi.DTOs
{
    public class SupportRequestDto
    {
        public Guid Id { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public string StatusName { get; set; }
        public Guid StatusId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UserCreatedName { get; set; }
        public Guid UserCreatedId { get; set; }
    }
}
