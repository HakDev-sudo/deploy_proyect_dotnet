namespace ArcheryAcademy.Application.DTOs.UserPlanDto;

public class UserPlanUpdateDto
{
    public int Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int? RemainingClasses { get; set; }
    public bool IsActive { get; set; }
}