using System;
using System.Collections.Generic;

namespace ArcheryAcademy.Infrastructure.Persistence.Models;

public partial class Payment
{
    public int Id { get; set; }

    public int BookingId { get; set; }

    public decimal Amount { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Booking Booking { get; set; } = null!;
}
