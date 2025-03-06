using HotelBookingApi.Models;

namespace HotelBookingApi.Repositories
{
    public interface IHotelRepository
    {
        IEnumerable<Hotel> GetAllHotels();
        void AddHotel(Hotel hotel);
    }
}
