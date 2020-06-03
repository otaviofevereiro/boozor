using Microsoft.Extensions.DependencyInjection;

namespace Curriculum.Business
{
    public static class Module
    {
        public static void UseBusinessLayer(this IServiceCollection services)
        {
            services.AddTransient(typeof(IEntityService<>), typeof(EntityService<>));
        }
    }
}
