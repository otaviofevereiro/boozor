using System.Linq.Expressions;
using Boozor.Shared;

namespace Boozor.Data;

public interface IRepository
{
    Task CreateAsync(Type entityType, IEntity entity, CancellationToken cancellationToken = default);

    Task UpdateAsync(Type entityType, IEntity entity, CancellationToken cancellationToken = default);

    Task DeleteAsync(Type entityType, string id, CancellationToken cancellationToken = default);

    Task<T?> GetAsync<T>(Type entityType, string id, CancellationToken cancellationToken = default);
    
    Task<T?> GetAsync<T>(Type entityType, Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
}
