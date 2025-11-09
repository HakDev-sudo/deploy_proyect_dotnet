namespace ArcheryAcademy.Application.Dtos.PlanDto;

public class PlanUpdateDto
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal? Price { get; set; }
    public int? NumClasses { get; set; }
    public int? DurationDays { get; set; }
    public bool? IsActive { get; set; }
}