using HotelBookingApi.Common;
using HotelBookingApi.DTOs;
using HotelBookingApi.Models;
using HotelBookingApi.Repositories;
using Microsoft.AspNetCore.Identity;

namespace HotelBookingApi.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly PasswordHasher<User> _passwordHasher;
        public UserService(IUserRepository repository)
        {
            _repository = repository;
            _passwordHasher = new PasswordHasher<User>();
        }

        public async Task<UserProfileDto?> GetUserProfileAsync(Guid userId)
        {
            var user = await _repository.GetUserByIdAsync(userId);
            if (user == null)
                return null;

            return new UserProfileDto
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                Phone = user.Phone,
                UserRole = user.UserRole
            };
        }

        public async Task<bool> UpdateUserProfileAsync(Guid userId, UserProfileUpdateDto updateDto)
        {
            var user = await _repository.GetUserByIdAsync(userId);
            if (user == null)
            {
                return false;
            }

            return await _repository.UpdateUserProfileAsync(userId, updateDto);
        }

        public async Task<Result<bool>> ChangePasswordAsync(Guid userId, string currentPassword, string newPassword)
        {
            var userExist = await _repository.GetUserByIdAsync(userId);
            if (userExist == null)
            {
                return Result<bool>.Failure("User not found.");
            }

            var user = new User
            {
                FullName = userExist.FullName,
                Email = userExist.Email,
                UserRoleId = userExist.UserRoleId,
                PasswordHash = userExist.PasswordHash
            };
           

            var passwordVerification = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, currentPassword);
            if (passwordVerification == PasswordVerificationResult.Failed)
                return Result<bool>.Failure("Current password is incorrect.");

            user.PasswordHash = _passwordHasher.HashPassword(user, newPassword);
            var updated = await _repository.UpdatePasswordAsync(userId, user.PasswordHash);

            return updated ? Result<bool>.Success(true) : Result<bool>.Failure("Failed to update password.");
        }
    }
}
