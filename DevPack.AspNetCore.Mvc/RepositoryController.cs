using DevPack.Data;
using DevPack.Data.Core;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DevPack.AspNetCore.Mvc
{
    public abstract class RepositoryController<TEntity, TId> : QueryController<TEntity, TId>
            where TEntity : Entity<TEntity, TId>
    {
        protected RepositoryController(IRepository<TEntity, TId> repository) : base(repository)
        {
        }

        [HttpDelete("{id}")]
        public Task DeleteAsync(TId id, CancellationToken cancellationToken = default)
        {
            return Repository.DeleteAsync(id, cancellationToken);
        }

        [HttpPost]
        public async Task<IResult> InsertAsync([FromBody] TEntity entity, CancellationToken cancellationToken = default)
        {
            var result = entity.Validate();

            if (result.IsInvalid)
                await Repository.InsertAsync(entity, cancellationToken);

            return result;
        }

        [HttpPost]
        public async Task<EntityResultCollection<TId>> InsertAsync([FromBody] IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            var results = new EntityResultCollection<TId>();

            foreach (var entity in entities)
            {
                var result = entity.Validate();

                if (result.IsValid)
                    await Repository.InsertAsync(entities, cancellationToken);
                else
                    results.Add(entity.Id, result);
            }

            return results;
        }

        [HttpPut]
        public async Task<IResult> SaveAsync([FromBody] TEntity entity, CancellationToken cancellationToken = default)
        {
            var result = entity.Validate();

            if (result.IsInvalid)
                await Repository.SaveAsync(entity, cancellationToken);

            return result;
        }

        [HttpPut]
        public async Task<EntityResultCollection<TId>> SaveAsync([FromBody] IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            var results = new EntityResultCollection<TId>();

            foreach (var entity in entities)
            {
                var result = entity.Validate();

                if (result.IsValid)
                    await Repository.SaveAsync(entities, cancellationToken);
                else
                    results.Add(entity.Id, result);
            }

            return results;
        }
    }
}
