using System;

namespace ArcheryAcademy.Domain.Entities;

public class GoogleToken
{

    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public string AccessToken { get; set; } = null!;

    public string? RefreshToken { get; set; }
    
    public string? TokenType { get; set; }

    public long? ExpiresInSeconds { get; set; }

    public DateTime IssuedUtc { get; set; }

    public string? Scope { get; set; }

    public DateTime? CreatedAt { get; set; }
    
    public DateTime? UpdatedAt { get; set; }

    public virtual User User { get; set; } = null!;
}

