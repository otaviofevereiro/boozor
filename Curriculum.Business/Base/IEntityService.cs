using Curriculum.Entities.Base;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Curriculum.Business
{
    public interface IEntityService<TEntity> where TEntity : Entity
    {
        Task<TEntity> Add(TEntity entity, CancellationToken cancellationToken = default);
        Task<IEnumerable<TEntity>> All(CancellationToken cancellationToken = default);
        Task<TEntity> Delete(int id, CancellationToken cancellationToken = default);
        Task<TEntity> Find(int id, CancellationToken cancellationToken = default);
        Task<TEntity> Update(TEntity entity, CancellationToken cancellationToken = default);
        IQueryable<TEntity> AsQueryable();
    }
}