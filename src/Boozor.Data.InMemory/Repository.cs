using System.Collections.Concurrent;
using Boozor.Shared;

namespace Boozor.Data.InMemory;

public class Repository : IRepository
{
    private readonly Lazy<ConcurrentDictionary<string, ConcurrentDictionary<string, object>>> lazyContainers = new(new ConcurrentDictionary<string, ConcurrentDictionary<string, object>>());

    public Task CreateAsync(Type entityType, IEntity entity, CancellationToken cancellationToken = default)
    {
        Validate(entityType, entity);

        entity.Id = Guid.NewGuid().ToString();

        var container = GetOrCreateContainer(entityType);

        if (container.TryAdd(entity.Id!, entity))
            return Task.CompletedTask;

        throw new InvalidOperationException($"Id already exists on container {entityType.FullName}");
    }


    public Task DeleteAsync(Type entityType, string id, CancellationToken cancellationToken = default)
    {
        Validate(entityType, id);

        var container = GetOrCreateContainer(entityType);

        if (container.Remove(id, out _))
            return Task.CompletedTask;

        throw new InvalidOperationException($"The Id does not exists on container {entityType.FullName}");
    }

    public Task<T?> GetAsync<T>(Type entityType, string id, CancellationToken cancellationToken = default)
    {
        Validate(entityType, id);

        var container = GetOrCreateContainer(entityType);

        if (container.TryGetValue(id, out object? entity))
            return Task.FromResult((T?)entity);

        return Task.FromResult(default(T));
    }

    public Task UpdateAsync(Type entityType, IEntity entity, CancellationToken cancellationToken = default)
    {
        Validate(entityType, entity);

        if (string.IsNullOrEmpty(entity.Id))
            throw new InvalidOperationException("Id cannot be empty");

        var container = GetOrCreateContainer(entityType);
        
        if (container.TryGetValue(entity.Id!, out object? currentEntity) &&
            container.TryUpdate(entity.Id!, entity, currentEntity))
            return Task.FromResult(entity);

        throw new InvalidOperationException($"The Id does not exists on container {entityType.FullName}");
    }

    private ConcurrentDictionary<string, object> GetOrCreateContainer(Type type)
    {
        var containerName = type.GetContainerId();

        return lazyContainers.Value.GetOrAdd(containerName, new ConcurrentDictionary<string, object>());
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
