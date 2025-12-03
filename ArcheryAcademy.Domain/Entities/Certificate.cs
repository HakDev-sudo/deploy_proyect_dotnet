
using System;

namespace ArcheryAcademy.Domain.Entities;

public class Certificate
{
    
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public string Type { get; set; } = null!;
    
    public string Title { get; set; } = null!;
    
    public string? Description { get; set; }


    public string VerificationCode { get; set; } = null!;

    public string? PdfUrl { get; set; }

    public string? BlobFileName { get; set; }

    public DateTime IssuedAt { get; set; }

    public Guid? IssuedById { get; set; }

    public DateTime? CreatedAt { get; set; }


    public virtual User User { get; set; } = null!;
    
    public virtual User? IssuedBy { get; set; }
}