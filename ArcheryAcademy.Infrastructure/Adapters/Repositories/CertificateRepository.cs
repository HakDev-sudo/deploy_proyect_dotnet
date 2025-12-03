using ArcheryAcademy.Domain.Entities;
using ArcheryAcademy.Domain.Ports.Repositories;
using ArcheryAcademy.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ArcheryAcademy.Infrastructure.Adapters.Repositories;

public class CertificateRepository : GenericRepository<Certificate>, ICertificateRepository
{
    public CertificateRepository(ArcheryAcademyDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Certificate>> GetByUserIdAsync(Guid userId)
    {
        return await _dbSet
            .Where(c => c.UserId == userId)
            .OrderByDescending(c => c.IssuedAt)
            .ToListAsync();
    }

    public async Task<Certificate?> GetByVerificationCodeAsync(string verificationCode)
    {
        return await _dbSet
            .Include(c => c.User)
            .FirstOrDefaultAsync(c => c.VerificationCode == verificationCode);
    }

    public async Task<bool> ExistsAsync(Guid userId, string certificateType)
    {
        return await _dbSet
            .AnyAsync(c => c.UserId == userId && c.Type == certificateType);
    }
}
