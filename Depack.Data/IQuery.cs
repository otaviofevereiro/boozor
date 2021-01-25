using DevPack.Data.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace DevPack.Data
{
    public interface IQuery<TEntity, TId>
          where TEntity : Entity<TId>

    {
        Task<IEnumerable<TEntity>> AllAsync(CancellationToken cancellationToken = default);

        IQueryable<TEntity> AsQueryable();

        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken = default);
    }
}
