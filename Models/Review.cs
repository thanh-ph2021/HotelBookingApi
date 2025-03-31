using System;
using System.Collections.Generic;

namespace HotelBookingApi.Models;

public partial class Review
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public Guid HotelId { get; set; }

    public string RoomType { get; set; } = null!;

    public int StayDuration { get; set; }

    public DateOnly StayMonth { get; set; }

    public decimal Rating { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? UsefulCount { get; set; }

    public int? NotUsefulCount { get; set; }

    public virtual Hotel Hotel { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
