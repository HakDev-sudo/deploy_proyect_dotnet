namespace ArcheryAcademy.Application.Dtos.PlanDto;

public class PlanReadDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int NumClasses { get; set; }
    public int DurationDays { get; set; }
    
    public bool IsActive { get; set; } 
    public DateTime? CreatedAt { get; set; }

    // Se omiten las colecciones (`UserPlans`) para evitar ciclos y mantener el DTO ligero, 
}