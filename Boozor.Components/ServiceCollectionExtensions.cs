using Boozor.Components.Common;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection UserBoozor(this IServiceCollection services)
        {
            services.AddSingleton(new AppState());

            return services;
        }
    }
}
