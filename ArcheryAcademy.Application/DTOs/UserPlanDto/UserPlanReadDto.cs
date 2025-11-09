namespace ArcheryAcademy.Application.DTOs.UserPlanDto;

public class UserPlanReadDto
{
    public Guid UserId { get; set; }
    public Guid PlanId { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public int? RemainingClasses { get; set; }
    
}