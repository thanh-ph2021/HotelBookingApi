namespace HotelBookingApi.Models.Requests
{
    public class CreateHotelRequest : HotelBaseRequest
    {
        public Guid OwnerId { get; set; }
    }
}
