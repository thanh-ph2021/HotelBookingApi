namespace HotelBookingApi.Models.Requests
{
    public class SRRequest
    {
        public Guid UserId { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
    }
}
