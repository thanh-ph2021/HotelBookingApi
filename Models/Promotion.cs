using System;
using System.Collections.Generic;

namespace HotelBookingApi.Models;

public partial class Promotion
{
    public Guid Id { get; set; }

    public Guid HotelId { get; set; }

    public decimal DiscountPercentage { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Hotel Hotel { get; set; } = null!;
}
