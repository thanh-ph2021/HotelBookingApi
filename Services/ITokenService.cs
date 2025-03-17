using HotelBookingApi.DTOs;
using HotelBookingApi.Models;

namespace HotelBookingApi.Services
{
    public interface ITokenService
    {
        AuthResponseDto GenerateToken(UserProfileDto user);
    }
}
