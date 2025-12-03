using ArcheryAcademy.API.Dtos.Request;
using ArcheryAcademy.Domain.Ports.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ArcheryAcademy.API.Controllers;

[ApiController]
[Route("api/[controller]")]
//[Authorize]
public class CertificateController : ControllerBase
{
    private readonly ICertificateService _certificateService;

    public CertificateController(ICertificateService certificateService)
    {
        _certificateService = certificateService;
    }

    
    /// Genera un nuevo certificado sin PDF (generación automática)
    [HttpPost]
    // [Authorize(Roles = "Admin")] 
    public async Task<IActionResult> Generate([FromBody] GenerateCertificateRequestDto request)
    {
        try
        {
            var title = request.Title ?? $"Certificado de {request.CertificateType}";
            
            var certificate = await _certificateService.GenerateCertificateAsync(
                request.UserId,
                request.CertificateType,
                title,
                request.Description,
                request.issuedById);

            return CreatedAtAction(
                nameof(GetByVerificationCode),
                new { code = certificate.VerificationCode },
                new
                {
                    certificate.Id,
                    certificate.UserId,
                    certificate.Type,
                    certificate.Title,
                    certificate.Description,
                    certificate.VerificationCode,
                    certificate.IssuedAt
                });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    
    // Genera un certificado con PDF personalizado 
    [HttpPost("with-pdf")]
    // [Authorize(Roles = "Admin")] 
    public async Task<IActionResult> GenerateWithPdf(
        [FromForm] Guid userId,
        [FromForm] string certificateType,
        [FromForm] string? title,
        [FromForm] string? description,
        [FromForm] Guid issuedById,
        IFormFile pdfFile)
    {
        try
        {
            // Validar que sea un PDF
            if (pdfFile == null || pdfFile.Length == 0)
                return BadRequest(new { message = "Debe proporcionar un archivo PDF." });

            if (!pdfFile.ContentType.Equals("application/pdf", StringComparison.OrdinalIgnoreCase))
                return BadRequest(new { message = "El archivo debe ser un PDF." });

            // Validar tamaño máximo (10 MB)
            const int maxSizeInBytes = 10 * 1024 * 1024;
            if (pdfFile.Length > maxSizeInBytes)
                return BadRequest(new { message = "El archivo PDF no puede superar los 10 MB." });

            var certificateTitle = title ?? $"Certificado de {certificateType}";

            await using var stream = pdfFile.OpenReadStream();
            
            var certificate = await _certificateService.GenerateCertificateWithPdfAsync(
                userId,
                certificateType,
                certificateTitle,
                description,
                issuedById,
                stream,
                pdfFile.FileName);

            return CreatedAtAction(
                nameof(GetByVerificationCode),
                new { code = certificate.VerificationCode },
                new
                {
                    certificate.Id,
                    certificate.UserId,
                    certificate.Type,
                    certificate.Title,
                    certificate.Description,
                    certificate.VerificationCode,
                    certificate.PdfUrl,
                    certificate.IssuedAt
                });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

   
    [HttpGet("verify/{code}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetByVerificationCode(string code)
    {
        var certificate = await _certificateService.GetByVerificationCodeAsync(code);

        if (certificate == null)
            return NotFound(new { message = "Certificado no encontrado o código inválido." });

        return Ok(new
        {
            certificate.Id,
            certificate.Type,
            certificate.Title,
            certificate.Description,
            certificate.VerificationCode,
            certificate.PdfUrl,
            certificate.IssuedAt,
            IsValid = true
        });
    }

   
    // Obtiene todos los certificados de un usuario
    [HttpGet("user/{userId:guid}")]
    public async Task<IActionResult> GetByUserId(Guid userId)
    {
        var certificates = await _certificateService.GetByUserIdAsync(userId);

        return Ok(certificates.Select(c => new
        {
            c.Id,
            c.Type,
            c.Title,
            c.Description,
            c.VerificationCode,
            c.PdfUrl,
            c.IssuedAt
        }));
    }


    // Verifica si un usuario ya tiene un tipo de certificado específico
    [HttpGet("exists")]
    public async Task<IActionResult> Exists([FromQuery] Guid userId, [FromQuery] string certificateType)
    {
        var exists = await _certificateService.ExistsAsync(userId, certificateType);
        return Ok(new { exists });
    }
}
