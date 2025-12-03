using ArcheryAcademy.Domain.Entities;
using ArcheryAcademy.Domain.Ports.Repositories;
using ArcheryAcademy.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ArcheryAcademy.Infrastructure.Adapters.Repositories;

public class GoogleTokenRepository : GenericRepository<GoogleToken>, IGoogleTokenRepository
{
    public GoogleTokenRepository(ArcheryAcademyDbContext context) : base(context)
    {
    }

    public async Task<GoogleToken?> GetByUserIdAsync(Guid userId)
    {
        return await _context.GoogleTokens
            .FirstOrDefaultAsync(t => t.UserId == userId);
    }

    public async Task<bool> UserHasTokenAsync(Guid userId)
    {
        return await _context.GoogleTokens
            .AnyAsync(t => t.UserId == userId);
    }

    public async Task<GoogleToken> UpsertAsync(GoogleToken token)
    {
        var existingToken = await GetByUserIdAsync(token.UserId);

        if (existingToken != null)
        {
            // Actualizar token existente
            existingToken.AccessToken = token.AccessToken;
            existingToken.RefreshToken = token.RefreshToken ?? existingToken.RefreshToken;
            existingToken.TokenType = token.TokenType;
            existingToken.ExpiresInSeconds = token.ExpiresInSeconds;
            existingToken.IssuedUtc = token.IssuedUtc;
            existingToken.Scope = token.Scope;
            existingToken.UpdatedAt = DateTime.UtcNow;

            _context.GoogleTokens.Update(existingToken);
            await _context.SaveChangesAsync();
            return existingToken;
        }
        else
        {
            // Crear nuevo token
            token.Id = Guid.NewGuid();
            token.CreatedAt = DateTime.UtcNow;
            
            await _context.GoogleTokens.AddAsync(token);
            await _context.SaveChangesAsync();
            return token;
        }
    }
}
