using HotelBookingApi.DTOs;
using System.Collections;

namespace HotelBookingApi.Services
{
    public interface IHotelService
    {
        IEnumerable<HotelDto> GetHotels();
        void CreateHotel(HotelDto hotel);
    }
}
