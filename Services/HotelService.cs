using HotelBookingApi.DTOs;
using HotelBookingApi.Models;
using HotelBookingApi.Repositories;

namespace HotelBookingApi.Services
{
    public class HotelService : IHotelService
    {
        private readonly IHotelRepository _repository;

        public HotelService(IHotelRepository repository)
        {
            _repository = repository;
        }

        public void CreateHotel(HotelDto hotelDto)
        {
            var hotel = new Hotel
            {
                Name = hotelDto.Name,
                Address = hotelDto.Address
            };

            _repository.AddHotel(hotel);
        }

        public IEnumerable<HotelDto> GetHotels()
        {
            return _repository.GetAllHotels()
                .Select(h => new HotelDto
                {
                    Name = h.Name,
                    Address = h.Address
                });
        }
    }
}
