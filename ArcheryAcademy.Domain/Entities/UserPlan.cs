using System;
using System.Collections.Generic;

namespace ArcheryAcademy.Domain.Entities;

public partial class UserPlan
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public Guid PlanId { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public int? RemainingClasses { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual Plan Plan { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
