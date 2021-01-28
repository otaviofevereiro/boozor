using DevPack.Data.Core;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace DevPack.Data
{
    public interface IRepository<TEntity, TId> : IQuery<TEntity, TId>
         where TEntity : Entity<TEntity, TId>
    {
        Task DeleteAsync(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken = default);
        Task DeleteAsync(TId id, CancellationToken cancellationToken = default);
        Task InsertAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task InsertAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
        Task SaveAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task SaveAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
        Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task UpdateAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
    }
}
