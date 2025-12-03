using ArcheryAcademy.Domain.Entities;
using ArcheryAcademy.Domain.Ports.Repositories;
using ArcheryAcademy.Domain.Ports.Services;

namespace ArcheryAcademy.Infrastructure.Adapters.Services;

public class CertificateService : ICertificateService
{
    private readonly ICertificateRepository _certificateRepository;
    private readonly IBlobStorageService _blobStorageService;

    public CertificateService(
        ICertificateRepository certificateRepository,
        IBlobStorageService blobStorageService)
    {
        _certificateRepository = certificateRepository;
        _blobStorageService = blobStorageService;
    }

    public async Task<Certificate> GenerateCertificateAsync(
        Guid userId, 
        string certificateType, 
        string title, 
        string? description, 
        Guid issuedById)
    {
        // Verificar si ya existe un certificado de este tipo para el usuario
        var exists = await _certificateRepository.ExistsAsync(userId, certificateType);
        if (exists)
        {
            throw new InvalidOperationException(
                $"El usuario ya tiene un certificado de tipo '{certificateType}'");
        }

        // Generar código de verificación único
        var verificationCode = GenerateVerificationCode();

        var certificate = new Certificate
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Type = certificateType,
            Title = title,
            Description = description,
            VerificationCode = verificationCode,
            IssuedAt = DateTime.UtcNow,
            IssuedById = issuedById,
            CreatedAt = DateTime.UtcNow
        };

        await _certificateRepository.AddAsync(certificate);

        return certificate;
    }

    public async Task<Certificate> GenerateCertificateWithPdfAsync(
        Guid userId,
        string certificateType,
        string title,
        string? description,
        Guid issuedById,
        Stream pdfStream,
        string pdfFileName)
    {
        // Verificar si ya existe un certificado de este tipo para el usuario
        var exists = await _certificateRepository.ExistsAsync(userId, certificateType);
        if (exists)
        {
            throw new InvalidOperationException(
                $"El usuario ya tiene un certificado de tipo '{certificateType}'");
        }

        // Generar código de verificación único
        var verificationCode = GenerateVerificationCode();

        // Subir PDF a Azure Blob Storage
        var blobFileName = $"cert_{userId}_{verificationCode}_{pdfFileName}";
        var pdfUrl = await _blobStorageService.UploadAsync(pdfStream, blobFileName, "application/pdf");

        var certificate = new Certificate
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Type = certificateType,
            Title = title,
            Description = description,
            VerificationCode = verificationCode,
            PdfUrl = pdfUrl,
            BlobFileName = blobFileName,
            IssuedAt = DateTime.UtcNow,
            IssuedById = issuedById,
            CreatedAt = DateTime.UtcNow
        };

        await _certificateRepository.AddAsync(certificate);

        return certificate;
    }

    public async Task<Certificate?> GetByVerificationCodeAsync(string verificationCode)
    {
        return await _certificateRepository.GetByVerificationCodeAsync(verificationCode);
    }

    public async Task<IEnumerable<Certificate>> GetByUserIdAsync(Guid userId)
    {
        return await _certificateRepository.GetByUserIdAsync(userId);
    }

    public async Task<bool> ExistsAsync(Guid userId, string certificateType)
    {
        return await _certificateRepository.ExistsAsync(userId, certificateType);
    }

    /// <summary>
    /// Genera un código de verificación único de 12 caracteres alfanuméricos
    /// Formato: XXXX-XXXX-XXXX
    /// </summary>
    private static string GenerateVerificationCode()
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        var random = new Random();
        var code = new char[12];
        
        for (int i = 0; i < 12; i++)
        {
            code[i] = chars[random.Next(chars.Length)];
        }

        // Formato: XXXX-XXXX-XXXX
        return $"{new string(code, 0, 4)}-{new string(code, 4, 4)}-{new string(code, 8, 4)}";
    }
}
