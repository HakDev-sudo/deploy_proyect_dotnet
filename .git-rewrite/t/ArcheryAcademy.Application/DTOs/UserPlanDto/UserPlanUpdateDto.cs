namespace ArcheryAcademy.Application.DTOs.UserPlanDto;

public class UserPlanUpdateDto
{
    public Guid Id { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public int? RemainingClasses { get; set; }
    public bool IsActive { get; set; }
}