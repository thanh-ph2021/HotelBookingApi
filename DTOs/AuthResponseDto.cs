namespace HotelBookingApi.DTOs
{
    public class AuthResponseDto
    {
        public string Token { get; set; } = null!;
        public DateTime Expiration { get; set; }
        public UserProfileDto2 userProfile { get; set; } = null!;
    }
}
