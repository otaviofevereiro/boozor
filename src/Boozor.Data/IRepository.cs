namespace Boozor.Data;

public interface IRepository
{
    Task CreateAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default);
    Task UpdateAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default);

}
