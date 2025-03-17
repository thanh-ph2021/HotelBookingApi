using HotelBookingApi.DTOs;
using HotelBookingApi.Models;

namespace HotelBookingApi.Repositories
{
    public interface IUserRepository
    {
        Task<UserProfileDto?> GetUserByIdAsync(Guid userId);
        Task<bool> UpdateUserProfileAsync(Guid userId, UserProfileUpdateDto userProfileUpdate);
        Task<bool> UpdatePasswordAsync(Guid userId, string newPasswordHash);
    }
}
