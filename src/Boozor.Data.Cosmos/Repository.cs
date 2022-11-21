using Microsoft.Azure.Cosmos;
using Boozor.Shared;

namespace Boozor.Data;

public sealed class Repository
{
    private readonly Uow _uow;

    public Repository(Uow uow)
    {
        _uow = uow;
    }

    public async Task UpsertAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default)
        where TEntity : IEntity
    {
        Container container = await GetContainerAsync<TEntity>();

        TEntity createdItem = await container.UpsertItemAsync<TEntity>(
            item: entity,
            partitionKey: new PartitionKey(entity.Id!)
        );
    }

    public async Task DeleteAsync<TEntity>(string id, CancellationToken cancellationToken = default)
        where TEntity : IEntity
    {
        Container container = await GetContainerAsync<TEntity>();

        TEntity createdItem = await container.DeleteItemAsync<TEntity>(
            id: id,
            partitionKey: new PartitionKey(id)
        );
    }

    public async Task GetAsync<TEntity>(string id, CancellationToken cancellationToken = default)
        where TEntity : IEntity
    {
        Container container = await GetContainerAsync<TEntity>();

        TEntity createdItem = await container.ReadItemAsync<TEntity>(
            id: id,
            partitionKey: new PartitionKey(id)
        );
    }

    private ValueTask<Container> GetContainerAsync<TEntity>() where TEntity : IEntity
    {
        ContainerOptions options = new()
        {
            Id = TEntity.EntityName
        };

        return _uow.GetContainerAsync(options);
    }
}


