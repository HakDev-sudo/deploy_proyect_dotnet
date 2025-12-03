namespace ArcheryAcademy.Application.DTOs.RolDto;

public class RolUpdateDto
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }
}