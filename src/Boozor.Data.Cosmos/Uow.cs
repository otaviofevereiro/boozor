using Microsoft.Azure.Cosmos;

namespace Boozor.Data;

public sealed class Uow
{
    private readonly Database _database;
    private readonly SemaphoreSlim _semaphoreContainer = new(1);
    private readonly AsyncValueFactory<ContainerOptions, Container> _containers;

    public Uow(Database database)
    {
        _containers = new(async options => await CreateContainerAsync(options));

        CosmosClient _client = new(accountEndpoint: Environment.GetEnvironmentVariable("COSMOS_ENDPOINT")!,
                                   authKeyOrResourceToken: Environment.GetEnvironmentVariable("COSMOS_KEY")!);

        _database = database;
    }

    public ValueTask<Container> GetContainerAsync(ContainerOptions options)
    {
        return _containers.GetOrCreateAsync(options);
    }


    private async Task<Container> CreateContainerAsync(ContainerOptions options)
    {
        try
        {
            return await InternalCreateConteinerAsync(options);
        }
        catch (Exception) //TODO: not exists exception
        {
            return await InternalCreateConteinerAsync(options);
        }
    }

    private async Task<Container> InternalCreateConteinerAsync(ContainerOptions options)
    {
        try
        {
            await _semaphoreContainer.WaitAsync();

            return await _database.CreateContainerIfNotExistsAsync(
                id: options.Id,
                partitionKeyPath: options.PartitionKeyPath
            );
        }
        finally
        {
            _semaphoreContainer.Release();
        }
    }
}
