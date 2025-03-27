namespace HotelBookingApi.Models.Requests
{
    public class UpdateRoomRequest : RoomBaseRequest
    {
        public Guid Id { get; set; }
    }
}
