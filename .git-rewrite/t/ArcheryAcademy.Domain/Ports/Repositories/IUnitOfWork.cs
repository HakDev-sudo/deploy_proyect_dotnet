namespace ArcheryAcademy.Domain.Ports;

public interface IUnitOfWork: IDisposable
{
    IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class;
    Task<int> CompleteAsync( CancellationToken cancellationToken = default);
}