using System;
using System.Collections.Generic;

namespace HotelBookingApi.Models;

public partial class Status
{
    public Guid Id { get; set; }

    public Guid StatusTypeId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual StatusType StatusType { get; set; } = null!;

    public virtual ICollection<SupportRequest> SupportRequests { get; set; } = new List<SupportRequest>();
}
