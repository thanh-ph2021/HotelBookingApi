using System;
using System.Collections.Generic;

namespace HotelBookingApi.Models;

public partial class Payment
{
    public Guid Id { get; set; }

    public Guid BookingId { get; set; }

    public Guid UserId { get; set; }

    public decimal Amount { get; set; }

    public Guid PaymentMethodId { get; set; }

    public Guid StatusId { get; set; }

    public string? TransactionId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? PaidAt { get; set; }

    public virtual Booking Booking { get; set; } = null!;

    public virtual PaymentMethod PaymentMethod { get; set; } = null!;

    public virtual Status Status { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
