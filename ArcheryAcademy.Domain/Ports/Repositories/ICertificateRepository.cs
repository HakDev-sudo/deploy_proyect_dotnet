using ArcheryAcademy.Domain.Entities;

namespace ArcheryAcademy.Domain.Ports.Repositories;

public interface ICertificateRepository : IGenericRepository<Certificate>
{
    Task<IEnumerable<Certificate>> GetByUserIdAsync(Guid userId);
    Task<Certificate?> GetByVerificationCodeAsync(string verificationCode);
    Task<bool> ExistsAsync(Guid userId, string certificateType);
}
