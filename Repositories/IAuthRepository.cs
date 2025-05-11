using HotelBookingApi.DTOs;
using HotelBookingApi.Models;

namespace HotelBookingApi.Repositories
{
    public interface IAuthRepository
    {
        Task<UserProfileDto?> GetUserByEmailAsync(string email);
        Task<UserProfileDto?> GetUserByFacebookIdAsync(string facebookId);
        Task<User> CreateUserAsync(User user);
    }
}
