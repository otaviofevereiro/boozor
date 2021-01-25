using DevPack.Data;
using DevPack.Data.Mongo;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class MongoExtensions
    {
        public static IServiceCollection AddMongoDb(this IServiceCollection services, string databaseName, string connectionString)
        {
            services.AddSingleton(x =>
            {
                var client = new MongoClient(connectionString);

                return client.GetDatabase(databaseName);
            });

            return services;
        }

        public static IServiceCollection AddMongoRepository<TEntity>(this IServiceCollection services, string collectionName)
            where TEntity : MongoEntity
        {
            services.AddTransient<IRepository<TEntity, string>>(sp =>
            {
                return new Repository<TEntity>(collectionName, sp.GetRequiredService<IMongoDatabase>());
            });

            return services;
        }
    }
}
