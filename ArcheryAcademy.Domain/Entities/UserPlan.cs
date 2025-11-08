using System;
using System.Collections.Generic;

namespace ArcheryAcademy.Infrastructure.Persistence.Models;

public partial class UserPlan
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int PlanId { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public int? RemainingClasses { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual Plan Plan { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
