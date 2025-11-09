using System;
using System.Collections.Generic;
using ArcheryAcademy.Domain.Enums;

namespace ArcheryAcademy.Infrastructure.Persistence.Entities;

public partial class Payment
{
    public Guid Id { get; set; }

    public Guid BookingId { get; set; }

    public decimal Amount { get; set; }
    
    public PaymentStatus PaymentStatus { get; set; }
    public PaymentMethod PaymentMethod { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Booking Booking { get; set; } = null!;
}
