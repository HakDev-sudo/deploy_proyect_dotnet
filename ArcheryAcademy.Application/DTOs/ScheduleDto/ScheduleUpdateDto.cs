namespace ArcheryAcademy.Application.DTOs.ScheduleDto;

public class ScheduleUpdateDto
{
    public Guid Id { get; set; }
    public int InstructorId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public int MaxStudents { get; set; }
    public bool IsActive { get; set; }
}