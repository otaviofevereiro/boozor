using System.Linq.Expressions;
using Boozor.Shared;

namespace Boozor.Data;

public interface IRepository<TEntity>
    where TEntity : IEntity
{
    Task CreateAsync(TEntity entity, CancellationToken cancellationToken = default);

    Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

    Task DeleteAsync(string id, CancellationToken cancellationToken = default);

    Task<TEntity?> GetAsync(string id, CancellationToken cancellationToken = default);

    Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
}