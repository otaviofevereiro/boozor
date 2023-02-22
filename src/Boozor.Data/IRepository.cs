using Boozor.Shared;

namespace Boozor.Data;

public interface IRepository
{
    Task CreateAsync(Type entityType, IEntity entity, CancellationToken cancellationToken = default);

    Task UpdateAsync(Type entityType, IEntity entity, CancellationToken cancellationToken = default);

    Task DeleteAsync(Type entityType, string id, CancellationToken cancellationToken = default);

    Task<T> GetAsync<T>(Type entityType, string id, CancellationToken cancellationToken = default);
}

public interface IRepository<TEntity>
    where TEntity : IEntity
{
    Task CreateAsync(TEntity entity, CancellationToken cancellationToken = default);

    Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

    Task DeleteAsync(string id, CancellationToken cancellationToken = default);

    Task<TEntity> GetAsync(string id, CancellationToken cancellationToken = default);
    
}
