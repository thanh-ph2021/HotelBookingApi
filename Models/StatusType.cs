using System;
using System.Collections.Generic;

namespace HotelBookingApi.Models;

public partial class StatusType
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Status> Statuses { get; set; } = new List<Status>();
}
