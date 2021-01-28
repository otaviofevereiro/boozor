using DevPack.Data;
using DevPack.Data.Core;
using DevPack.Data.Mongo;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class MongoExtensions
    {
        public static IServiceCollection AddMongoDb(this IServiceCollection services, string databaseName, string connectionString)
        {
            services.AddSingleton(x =>
            {

                var pack = new ConventionPack();
                pack.Add(new StringObjectIdConvention());

                ConventionRegistry.Register("MyConventions", pack, _ => true);

                var client = new MongoClient(connectionString);
                return client.GetDatabase(databaseName);
            });

            return services;
        }

        public static IServiceCollection AddMongoRepository<TEntity, TId>(this IServiceCollection services, string collectionName)
            where TEntity : Entity<TEntity, TId>
        {
            services.AddTransient<IRepository<TEntity, TId>>(sp =>
            {
                return new Repository<TEntity, TId>(collectionName, sp.GetRequiredService<IMongoDatabase>());
            });

            return services;
        }
    }
}
