using HotelBookingApi.DTOs;

namespace HotelBookingApi.Services
{
    public interface IFacebookAuthService
    {
        Task<FacebookUserInfo> GetUserInfoAsync(string accessToken);
    }
}
