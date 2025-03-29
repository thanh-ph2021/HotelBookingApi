using System;
using System.Collections.Generic;

namespace HotelBookingApi.Models;

public partial class Room
{
    public Guid Id { get; set; }

    public Guid HotelId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public decimal PricePerNight { get; set; }

    public int Capacity { get; set; }

    public string? Amenities { get; set; }

    public int? TotalRooms { get; set; }

    public int? AvailableRooms { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public bool IsDeleted { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual Hotel Hotel { get; set; } = null!;

    public virtual ICollection<RoomImage> RoomImages { get; set; } = new List<RoomImage>();
}
