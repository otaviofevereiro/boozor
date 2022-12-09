using Microsoft.Azure.Cosmos;

namespace Boozor.Data;

public sealed class Uow
{
    private readonly Database _database;
    private readonly SemaphoreSlim _semaphoreContainer = new(1);
    private readonly AsyncValueFactory<ContainerOptions, Container> _containers;

    public Uow(Database database)
    {
        _containers = new(async (options, cancellationToken) => await CreateContainerAsync(options, cancellationToken));

        CosmosClient _client = new(accountEndpoint: Environment.GetEnvironmentVariable("COSMOS_ENDPOINT")!,
                                   authKeyOrResourceToken: Environment.GetEnvironmentVariable("COSMOS_KEY")!);

        _database = database;
    }

    internal ValueTask<Container> GetContainerAsync(ContainerOptions options, CancellationToken cancellationToken)
    {
        return _containers.GetOrCreateAsync(options, cancellationToken);
    }


    private async Task<Container> CreateContainerAsync(ContainerOptions options, CancellationToken cancellationToken)
    {
        try
        {
            return await InternalCreateConteinerAsync(options, cancellationToken);
        }
        catch (Exception) //TODO: not exists exception
        {
            return await InternalCreateConteinerAsync(options, cancellationToken);
        }
    }

    private async Task<Container> InternalCreateConteinerAsync(ContainerOptions options, CancellationToken cancellationToken)
    {
        try
        {
            await _semaphoreContainer.WaitAsync(cancellationToken);

            return await _database.CreateContainerIfNotExistsAsync(
                id: options.Id,
                partitionKeyPath: options.PartitionKeyPath,
                cancellationToken: cancellationToken
            );
        }
        finally
        {
            _semaphoreContainer.Release();
        }
    }
}
