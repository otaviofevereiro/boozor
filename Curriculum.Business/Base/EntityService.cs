using Curriculum.Entities.Base;
using Curriculum.Server.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Curriculum.Business
{
    public class EntityService<TEntity>
        where TEntity : Entity
    {
        private readonly CurriculumContext context;

        public EntityService(CurriculumContext context)
        {
            this.context = context;
        }

        public async Task<TEntity> Add(TEntity entity, CancellationToken cancellationToken = default)
        {
            context.Add(entity);

            await context.SaveChangesAsync(cancellationToken);

            return entity;
        }

        public async Task<IEnumerable<TEntity>> All(CancellationToken cancellationToken = default)
        {
            return await EntityFrameworkQueryableExtensions.ToListAsync(context.Set<TEntity>().AsNoTracking(), cancellationToken);
        }

        public async Task<TEntity> Delete(int id, CancellationToken cancellationToken = default)
        {
            var set = context.Set<TEntity>();
            var entity = set.Find(id);

            set.Remove(entity);

            await context.SaveChangesAsync(cancellationToken);

            return entity;
        }

        public async Task<TEntity> Find(int id, CancellationToken cancellationToken = default)
        {
            return await context.Set<TEntity>().AsNoTracking().SingleAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<TEntity> Update(TEntity entity, CancellationToken cancellationToken = default)
        {
            context.Attach(entity);

            await context.SaveChangesAsync(cancellationToken);

            return entity;
        }
    }
}
