using System;
using System.Collections.Generic;

namespace HotelBookingApi.Models;

public partial class SupportRequest
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public string Subject { get; set; } = null!;

    public string Message { get; set; } = null!;

    public Guid StatusId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Status Status { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
