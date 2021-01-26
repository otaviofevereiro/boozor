using DevPack.Data;
using DevPack.Data.Core;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DevPack.AspNetCore.Mvc
{
    public abstract class QueryController<TEntity, TId> : ControllerBase
            where TEntity : Entity<TEntity, TId>
    {
        protected readonly IRepository<TEntity, TId> Repository;


        protected QueryController(IRepository<TEntity, TId> repository)
        {
            Repository = repository;
        }

        [HttpGet]
        public async Task<IEnumerable<TEntity>> AllAsync(CancellationToken cancellationToken = default)
        {
            return await Repository.AllAsync(cancellationToken);
        }

        [HttpGet("{id}")]
        public async Task<TEntity> FindAsync(TId id, CancellationToken cancellationToken = default)
        {
            return await Repository.FindAsync(id, cancellationToken);
        }
    }
}
