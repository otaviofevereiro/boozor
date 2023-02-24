using System.Linq.Expressions;
using Boozor.Shared;

namespace Boozor.Data;

public sealed class Repository<TEntity> : IRepository<TEntity>
    where TEntity : IEntity
{
    private readonly static Type entityType = typeof(TEntity);
    private readonly IRepository repository;

    public Repository(IRepository repository)
    {
        this.repository = repository;
    }

    public Task CreateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        return repository.CreateAsync(entityType, entity, cancellationToken);
    }

    public Task DeleteAsync(string id, CancellationToken cancellationToken = default)
    {
        return repository.DeleteAsync(entityType, id, cancellationToken);
    }

    public Task<TEntity?> GetAsync(string id, CancellationToken cancellationToken = default)
    {
        return repository.GetAsync<TEntity?>(entityType, id, cancellationToken);
    }

    public Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return repository.GetAsync(entityType, predicate, cancellationToken);
    }

    public Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        return repository.UpdateAsync(entityType, entity, cancellationToken);
    }
}
