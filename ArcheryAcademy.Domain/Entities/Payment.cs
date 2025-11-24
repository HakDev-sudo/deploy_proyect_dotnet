using System;
using System.Collections.Generic;

namespace ArcheryAcademy.Domain.Entities;

public partial class Payment
{
    public Guid Id { get; set; }

    public Guid BookingId { get; set; }

    public decimal Amount { get; set; }

    public int MethodId { get; set; }

    public int StatusId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Booking Booking { get; set; } = null!;

    public virtual PaymentMethod Method { get; set; } = null!;

    public virtual PaymentStatus Status { get; set; } = null!;
}
