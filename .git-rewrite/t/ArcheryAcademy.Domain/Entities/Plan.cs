using System;
using System.Collections.Generic;

namespace ArcheryAcademy.Domain.Entities;

public partial class Plan
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public int? NumClasses { get; set; }

    public int? DurationDays { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<UserPlan> UserPlans { get; set; } = new List<UserPlan>();
}
