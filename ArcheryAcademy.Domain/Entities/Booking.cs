using System;
using System.Collections.Generic;
using ArcheryAcademy.Domain.Enums;

namespace ArcheryAcademy.Infrastructure.Persistence.Entities;

public partial class Booking
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public Guid ScheduleId { get; set; }

    public Guid UserPlanId { get; set; }
    
    // Mapea a la columna 'status' de tipo 'booking_status'
    public BookingStatus Status { get; set; }

    // Mapea a la columna 'payment_status' de tipo 'payment_status'
    public PaymentStatus PaymentStatus { get; set; }


    public DateTime? AttendedAt { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual Schedule Schedule { get; set; } = null!;

    public virtual User User { get; set; } = null!;

    public virtual UserPlan UserPlan { get; set; } = null!;
}
