namespace ArcheryAcademy.Application.Dtos.PlanDto;

public class PlanCreateDto
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int NumClasses { get; set; }
    public int DurationDays { get; set; }
}