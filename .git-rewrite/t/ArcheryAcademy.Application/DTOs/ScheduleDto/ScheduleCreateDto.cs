namespace ArcheryAcademy.Application.DTOs.ScheduleDto;

public class ScheduleCreateDto
{
    public Guid InstructorId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public int MaxStudents { get; set; }

}