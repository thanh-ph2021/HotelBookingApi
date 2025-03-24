using System;
using System.Collections.Generic;

namespace HotelBookingApi.Models;

public partial class Hotel
{
    public Guid Id { get; set; }

    public Guid OwnerId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string Address { get; set; } = null!;

    public string? City { get; set; }

    public string? Country { get; set; }

    public decimal? Latitude { get; set; }

    public decimal? Longitude { get; set; }

    public decimal? Rating { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public bool IsDeleted { get; set; }

    public virtual ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();

    public virtual ICollection<HotelImage> HotelImages { get; set; } = new List<HotelImage>();

    public virtual User Owner { get; set; } = null!;

    public virtual ICollection<Promotion> Promotions { get; set; } = new List<Promotion>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    public virtual ICollection<Room> Rooms { get; set; } = new List<Room>();
}
