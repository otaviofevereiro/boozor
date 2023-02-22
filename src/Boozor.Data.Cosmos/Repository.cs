using Microsoft.Azure.Cosmos;
using Boozor.Shared;

namespace Boozor.Data;




public sealed class Repository : IRepository
{
    private readonly Uow _uow;

    public Repository(Uow uow)
    {
        _uow = uow;
    }

    public async Task CreateAsync(Type entityType, IEntity entity, CancellationToken cancellationToken = default)
    {
        Validate(entityType, entity);

        entity.Id = Guid.NewGuid().ToString();

        Container container = await GetContainerAsync(entityType, cancellationToken);

        await container.CreateItemAsync<object>(
            item: entity,
            partitionKey: new PartitionKey(entity.Id),
            cancellationToken: cancellationToken
        );
    }

    public async Task UpdateAsync(Type entityType, IEntity entity, CancellationToken cancellationToken = default)
    {
        Validate(entityType, entity);

        if (string.IsNullOrEmpty(entity.Id))
            throw new InvalidOperationException("Id cannot be empty");

        Container container = await GetContainerAsync(entityType, cancellationToken);

        await container.UpsertItemAsync<object>(
            item: entity,
            partitionKey: new PartitionKey(entity.Id!),
            cancellationToken: cancellationToken
        );
    }

    public async Task DeleteAsync(Type entityType, string id, CancellationToken cancellationToken = default)
    {
        Validate(entityType, id);

        Container container = await GetContainerAsync(entityType, cancellationToken);

        await container.DeleteItemAsync<object>(
            id: id,
            partitionKey: new PartitionKey(id),
            cancellationToken: cancellationToken
        );
    }

    public async Task<T> GetAsync<T>(Type entityType, string id, CancellationToken cancellationToken = default)
    {
        Validate(entityType, id);

        Container container = await GetContainerAsync(entityType, cancellationToken);

        return await container.ReadItemAsync<T>(
            id: id,
            partitionKey: new PartitionKey(id),
            cancellationToken: cancellationToken
        );
    }

    private ValueTask<Container> GetContainerAsync(Type entityType, CancellationToken cancellationToken)
    {
        ContainerOptions options = new()
        {
            Id = entityType.GetContainerId()
        };

        return _uow.GetContainerAsync(options, cancellationToken);
    }

    private static void Validate(Type entityType, string id)
    {
        if (entityType is null)
            throw new ArgumentNullException(nameof(entityType));

        if (string.IsNullOrEmpty(id))
            throw new ArgumentNullException(nameof(id));
    }

    private static void Validate(Type entityType, IEntity entity)
    {
        if (entityType is null)
            throw new ArgumentNullException(nameof(entityType));

        if (entity is null)
            throw new ArgumentNullException(nameof(entity));
    }
}


