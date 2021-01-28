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
    public class Repository<TEntity, TId> : Query<TEntity, TId>, IRepository<TEntity, TId>
        where TEntity : Entity<TEntity, TId>

    {
        public Repository(string collectionName, IMongoDatabase mongoDatabase) : base(collectionName, mongoDatabase)
        {
        }

        public async Task DeleteAsync(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken = default)
        {
            await Collection.DeleteManyAsync<TEntity>(filter, cancellationToken);
        }

        public async Task DeleteAsync(TId id, CancellationToken cancellationToken = default)
        {
            var filter = Builders<TEntity>.Filter.Eq(x => x.Id, id);

            await Collection.DeleteOneAsync(filter, cancellationToken);
        }

        public async Task InsertAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            await Collection.InsertOneAsync(entity, new InsertOneOptions(), cancellationToken);
        }

        public async Task InsertAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            await Collection.InsertManyAsync(entities, cancellationToken: cancellationToken);
        }
        public async Task SaveAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            var filter = Builders<TEntity>.Filter.Eq(x => x.Id, entity.Id);

            await Collection.ReplaceOneAsync(filter,
                                             entity,
                                             options: new ReplaceOptions() { IsUpsert = true },
                                             cancellationToken: cancellationToken);

        }

        public async Task SaveAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            var replaceModel = entities.Select(record =>
                new ReplaceOneModel<TEntity>(
                    Builders<TEntity>.Filter.Eq(x => x.Id, record.Id),
                    record)
                {
                    IsUpsert = true
                });

            await Collection.BulkWriteAsync(replaceModel, cancellationToken: cancellationToken);
        }

        public async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            var filter = Builders<TEntity>.Filter.Eq(x => x.Id, entity.Id);

            await Collection.ReplaceOneAsync(filter,
                                             entity,
                                             cancellationToken: cancellationToken);
        }

        public async Task UpdateAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            var replaceModel = entities.Select(record =>
                new ReplaceOneModel<TEntity>(
                    Builders<TEntity>.Filter.Eq(x => x.Id, record.Id),
                    record)
                {
                    IsUpsert = false
                });

            await Collection.BulkWriteAsync(replaceModel, cancellationToken: cancellationToken);
        }
    }
}
