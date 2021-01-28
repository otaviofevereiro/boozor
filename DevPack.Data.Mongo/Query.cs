using DevPack.Data.Core;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace DevPack.Data.Mongo
{
    public class Query<TEntity, TId> : MongoBase<TEntity>, IQuery<TEntity, TId>
        where TEntity : Entity<TEntity, TId>
    {
        public Query(string collectionName, IMongoDatabase mongoDatabase) : base(collectionName, mongoDatabase)
        {
        }

        public async Task<IEnumerable<TEntity>> AllAsync(CancellationToken cancellationToken = default)
        {
            var cursor = await Collection.FindAsync(FilterDefinition<TEntity>.Empty, cancellationToken: cancellationToken);

            return cursor.ToEnumerable(cancellationToken: cancellationToken);
        }

        public IQueryable<TEntity> AsQueryable()
        {
            return Collection.AsQueryable();
        }

        public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken = default)
        {
            var cursor = await Collection.FindAsync(filter, cancellationToken: cancellationToken);

            return cursor.ToEnumerable(cancellationToken: cancellationToken);
        }

        public async Task<TEntity> FindAsync(TId id, CancellationToken cancellationToken = default)
        {
            var filter = Builders<TEntity>.Filter.Eq(x => x.Id, id);
            var cursor = await Collection.FindAsync(filter, cancellationToken: cancellationToken);

            return cursor.Single(cancellationToken: cancellationToken);
        }
    }
}
