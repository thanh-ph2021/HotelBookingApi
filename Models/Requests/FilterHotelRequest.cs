namespace HotelBookingApi.Models.Requests
{
    public class FilterHotelRequest
    {
        public string? City { get; set; }
        public string? Country { get; set; }
        public string? Rating { get; set; }
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
