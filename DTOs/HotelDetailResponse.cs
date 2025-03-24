using System.ComponentModel.DataAnnotations.Schema;

namespace HotelBookingApi.DTOs
{
    public class HotelDetailResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Rating { get; set; }
        public string Owner { get; set; }
        public string BannerImage { get; set; }
        public string ImageUrls { get; set; }
        [NotMapped]
        public List<string> ImageUrlList { get; set; }
        public int TotalCount { get; set; }
    }
}
