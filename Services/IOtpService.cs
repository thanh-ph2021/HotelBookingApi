using HotelBookingApi.Common;
using HotelBookingApi.DTOs;

namespace HotelBookingApi.Services
{
    public interface IOtpService
    {
        Task<Result<string>> GenerateOtpAsync(string email);
        Task<Result<AuthResponseDto>> VerifyOtpAndLoginAsync(string email, string otp);
    }
}
