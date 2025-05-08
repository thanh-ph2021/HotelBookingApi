using HotelBookingApi.Common;
using HotelBookingApi.Models;
using HotelBookingApi.Models.Requests;

namespace HotelBookingApi.Repositories
{
    public interface IOtpRepository
    {
        Task AddOtpAsync(OtpRequest otp);
        Task<OtpRequest?> GetValidOtpAsync(string email, string hashedOtp);
        Task SaveChangesAsync();
    }
}
