namespace ArcheryAcademy.Application.DTOs.ScheduleDto;

public class ScheduleReadDto
{
    public Guid Id { get; set; }
    public Guid InstructorId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public int? MaxStudents { get; set; }
    public bool? IsActive { get; set; }
    public DateTime? CreatedAt { get; set; }
}