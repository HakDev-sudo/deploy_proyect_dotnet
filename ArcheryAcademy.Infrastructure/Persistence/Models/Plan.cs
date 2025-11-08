using System;
using System.Collections.Generic;

namespace ArcheryAcademy.Infrastructure.Persistence.Models;

public partial class Plan
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public int NumClasses { get; set; }

    public int DurationDays { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<UserPlan> UserPlans { get; set; } = new List<UserPlan>();
}
