using System;
using System.Collections.Generic;

namespace ArcheryAcademy.Domain.Entities;

public partial class Booking
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public Guid ScheduleId { get; set; }

    public Guid UserPlanId { get; set; }

    public int StatusId { get; set; }

    public int PaymentStatusId { get; set; }

    public DateTime? AttendedAt { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual PaymentStatus PaymentStatus { get; set; } = null!;

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual Schedule Schedule { get; set; } = null!;

    public virtual BookingStatus Status { get; set; } = null!;

    public virtual User User { get; set; } = null!;

    public virtual UserPlan UserPlan { get; set; } = null!;
}
