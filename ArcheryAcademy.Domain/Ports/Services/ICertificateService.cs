using ArcheryAcademy.Domain.Entities;

namespace ArcheryAcademy.Domain.Ports.Services;

public interface ICertificateService
{
    /// <summary>
    /// Genera un certificado sin PDF (generación automática)
    /// </summary>
    Task<Certificate> GenerateCertificateAsync(Guid userId, string certificateType, string title, string? description, Guid issuedById);
    

    Task<Certificate> GenerateCertificateWithPdfAsync(
        Guid userId, 
        string certificateType, 
        string title, 
        string? description, 
        Guid issuedById,
        Stream pdfStream,
        string pdfFileName);
    
    Task<Certificate?> GetByVerificationCodeAsync(string verificationCode);
    Task<IEnumerable<Certificate>> GetByUserIdAsync(Guid userId);
    Task<bool> ExistsAsync(Guid userId, string certificateType);
}
