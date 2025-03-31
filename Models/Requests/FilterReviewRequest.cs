namespace HotelBookingApi.Models.Requests
{
    public class FilterReviewRequest
    {
        public Guid HotelId { get; set; }
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 10;

    }
}
