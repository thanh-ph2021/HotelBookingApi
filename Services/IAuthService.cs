using HotelBookingApi.Common;
using HotelBookingApi.DTOs;

namespace HotelBookingApi.Services
{
    public interface IAuthService
    {
        Task<Result<AuthResponseDto>> Register(RegisterDto registerDto);
        Task<Result<AuthResponseDto>> Login(LoginDto loginDto);
        Task Logout();
        Task<Result<bool>> ChangePassword(ChangePasswordDto changePasswordDto);

        Task<Result<string>> RequestOtp(string phoneNumber);
        Task<Result<AuthResponseDto>> VerifyOtp(VerifyOtpDto otpDto);
    }
}
