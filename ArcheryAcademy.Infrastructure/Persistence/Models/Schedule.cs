using System;
using System.Collections.Generic;

namespace ArcheryAcademy.Infrastructure.Persistence.Models;

public partial class Schedule
{
    public int Id { get; set; }

    public int InstructorId { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public int MaxStudents { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual User Instructor { get; set; } = null!;
}
