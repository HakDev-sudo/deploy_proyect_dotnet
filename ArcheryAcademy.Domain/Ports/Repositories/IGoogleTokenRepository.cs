using ArcheryAcademy.Domain.Entities;

namespace ArcheryAcademy.Domain.Ports.Repositories;

public interface IGoogleTokenRepository : IGenericRepository<GoogleToken>
{
    /// <summary>
    /// Obtiene el token de Google de un usuario
    /// </summary>
    Task<GoogleToken?> GetByUserIdAsync(Guid userId);

    /// <summary>
    /// Verifica si un usuario tiene token de Google
    /// </summary>
    Task<bool> UserHasTokenAsync(Guid userId);

    /// <summary>
    /// Actualiza o crea el token de un usuario
    /// </summary>
    Task<GoogleToken> UpsertAsync(GoogleToken token);
}
