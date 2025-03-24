namespace HotelBookingApi.Models.Requests
{
    public class HotelBaseRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string BannerImage { get; set; }
        public List<string> ImageUrls { get; set; }
    }
}
