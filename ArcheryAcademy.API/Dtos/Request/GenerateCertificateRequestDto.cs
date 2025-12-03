namespace ArcheryAcademy.API.Dtos.Request;

public record GenerateCertificateRequestDto
{
   public Guid UserId { get; init; }
   public string CertificateType { get; init; }
   public string? CourseName { get; init; }
   public string? Title { get; init; }
   public string? Description { get; init; }
   public Guid issuedById { get; init; }
}