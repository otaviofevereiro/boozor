using Boozor.Data.InMemory;
using Microsoft.Extensions.DependencyInjection;
using Boozor.Data;

namespace Boozor;


public static class InMemoryExtensions
{
    public static IServiceCollection AddInMemoryDatabase(this IServiceCollection services)
    {
        return services.AddSingleton<IRepository, Repository>();
    }
}
