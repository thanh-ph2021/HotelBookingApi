using HotelBookingApi.Common;
using HotelBookingApi.DTOs;

namespace HotelBookingApi.Services
{
    public interface IUserService
    {
        Task<UserProfileDto?> GetUserProfileAsync(Guid userId);
        Task<bool> UpdateUserProfileAsync(Guid userId, UserProfileUpdateDto updateDto);
        Task<Result<bool>> ChangePasswordAsync(Guid userId, string currentPassword, string newPassword);
    }
}
