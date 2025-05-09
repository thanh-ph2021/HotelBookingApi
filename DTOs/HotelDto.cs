﻿namespace HotelBookingApi.DTOs
{
    public class HotelDto
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
        public int TotalCount { get; set; }
    }
}
