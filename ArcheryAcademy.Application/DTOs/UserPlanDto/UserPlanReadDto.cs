namespace ArcheryAcademy.Application.DTOs.UserPlanDto;

public class UserPlanReadDto
{
    public int UserId { get; set; }
    public int PlanId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int? RemainingClasses { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
}