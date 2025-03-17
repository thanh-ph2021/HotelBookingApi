namespace HotelBookingApi.DTOs
{
    public class UserProfileDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string UserRole { get; set; } = null!;
        public Guid UserRoleId { get; set; }
        public string? Phone { get; set; }
        public string PasswordHash { get; set; } = null!;
    }
}
