using MongoDB.Driver;
using System;

namespace DevPack.Data.Mongo
{
    public class MongoBase<TEntity>
    {
        private readonly Lazy<IMongoCollection<TEntity>> _collectionLazy;

        public MongoBase(string collectionName, IMongoDatabase mongoDatabase)
        {
            CollectionName = collectionName;
            _collectionLazy = new Lazy<IMongoCollection<TEntity>>(() => mongoDatabase.GetCollection<TEntity>(collectionName));
        }

        public string CollectionName { get; }
        protected IMongoCollection<TEntity> Collection => _collectionLazy.Value;
    }
}
