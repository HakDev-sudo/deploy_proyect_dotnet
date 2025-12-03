namespace ArcheryAcademy.Domain.Entities;

public partial class User
{
    public Guid Id { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string? Phone { get; set; }

    public string Status { get; set; } = "A";

    public DateTime? CreatedAt { get; set; }

    public Guid? InstructorId { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual User? Instructor { get; set; }

    public virtual ICollection<User> InverseInstructor { get; set; } = new List<User>();

    public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();

    public virtual ICollection<UserPlan> UserPlans { get; set; } = new List<UserPlan>();

    public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();

    // Certificados recibidos por el usuario
    public virtual ICollection<Certificate> Certificates { get; set; } = new List<Certificate>();

    // Certificados emitidos por el usuario (si es instructor/admin)
    public virtual ICollection<Certificate> IssuedCertificates { get; set; } = new List<Certificate>();

    // Tokens de Google OAuth (para Google Calendar)
    public virtual ICollection<GoogleToken> GoogleTokens { get; set; } = new List<GoogleToken>();
}
