using Cdv.Data.Base;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace DevPack.Data.Mongo
{
    public class Query<TEntity> : MongoBase<TEntity>, IQuery<TEntity, string>
        where TEntity : Entity
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

        public async Task<TEntity> FindAsync(string id, CancellationToken cancellationToken = default)
        {
            var cursor = await Collection.FindAsync(x => x.Id == id, cancellationToken: cancellationToken);

            return cursor.Single(cancellationToken: cancellationToken);
        }
    }
}
