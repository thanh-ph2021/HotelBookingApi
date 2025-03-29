namespace HotelBookingApi.Models.Requests
{
    public class FilterBookingRequest
    {
        public Guid UserId { get; set; }
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
