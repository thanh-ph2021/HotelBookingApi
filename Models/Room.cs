using System;
using System.Collections.Generic;

namespace HotelBookingApi.Models;

public partial class Room
{
    public Guid Id { get; set; }

    public Guid HotelId { get; set; }

    public Guid RoomTypeId { get; set; }

    public string Name { get; set; } = null!;

    public decimal PricePerNight { get; set; }

    public int Capacity { get; set; }

    public string? Amenities { get; set; }

    public bool? IsAvailable { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual Hotel Hotel { get; set; } = null!;

    public virtual RoomType RoomType { get; set; } = null!;
}
