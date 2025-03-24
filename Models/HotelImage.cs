using System;
using System.Collections.Generic;

namespace HotelBookingApi.Models;

public partial class HotelImage
{
    public Guid Id { get; set; }

    public Guid HotelId { get; set; }

    public string ImageUrl { get; set; } = null!;

    public bool? IsBanner { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Hotel Hotel { get; set; } = null!;
}
