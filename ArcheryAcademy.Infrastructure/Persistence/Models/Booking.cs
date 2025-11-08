using System;
using System.Collections.Generic;

namespace ArcheryAcademy.Infrastructure.Persistence.Models;

public partial class Booking
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int ScheduleId { get; set; }

    public int? UserPlanId { get; set; }

    public DateTime? AttendedAt { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual Schedule Schedule { get; set; } = null!;

    public virtual User User { get; set; } = null!;

    public virtual UserPlan? UserPlan { get; set; }
}
