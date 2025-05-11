using HotelBookingApi.Common;
using HotelBookingApi.DTOs;

namespace HotelBookingApi.Services
{
    public interface IAuthService
    {
        Task<Result<AuthResponseDto>> Register(RegisterDto registerDto);
        Task<Result<AuthResponseDto>> Login(LoginDto loginDto);
        Task<Result<bool>> ChangePassword(ChangePasswordDto changePasswordDto);
        Task<Result<AuthResponseDto>> GoogleSignInAsync(string idToken);
        Task<Result<AuthResponseDto>> FacebookSignInAsync(string accessToken);
    }
}
