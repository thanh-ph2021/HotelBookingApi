using System;
using System.Collections.Generic;

namespace HotelBookingApi.Models;

public partial class OtpRequest
{
    public Guid Id { get; set; }

    public string Email { get; set; } = null!;

    public string OtpCode { get; set; } = null!;

    public DateTime ExpiresAt { get; set; }

    public bool? IsUsed { get; set; }

    public DateTime? CreatedAt { get; set; }
}
