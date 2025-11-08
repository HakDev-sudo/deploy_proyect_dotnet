namespace ArcheryAcademy.Application.DTOs.UserPlanDto;

public class UserPlanCreateDto
{
    public int PlanId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int? RemainingClasses { get; set; }
}