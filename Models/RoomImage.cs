using System;
using System.Collections.Generic;

namespace HotelBookingApi.Models;

public partial class RoomImage
{
    public Guid Id { get; set; }

    public Guid RoomId { get; set; }

    public string ImageUrl { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public virtual Room Room { get; set; } = null!;
}
