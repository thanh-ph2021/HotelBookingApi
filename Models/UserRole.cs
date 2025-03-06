using System;
using System.Collections.Generic;

namespace HotelBookingApi.Models;

public partial class UserRole
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
