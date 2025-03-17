using System.ComponentModel.DataAnnotations;

namespace HotelBookingApi.DTOs
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "FullName is required")]
        public string FullName { get; set; } = null!;
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; } = null!;
        [Required(ErrorMessage = "Password is required")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters")]
        public string Password { get; set; } = null!;
        public required Guid UserRoleId { get; set; }
        public required string UserRole { get; set; }
    }
}
