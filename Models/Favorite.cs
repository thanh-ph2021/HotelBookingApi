using System;
using System.Collections.Generic;

namespace HotelBookingApi.Models;

public partial class Favorite
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public Guid HotelId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Hotel Hotel { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
