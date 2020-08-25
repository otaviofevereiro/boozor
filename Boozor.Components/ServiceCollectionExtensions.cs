using Boozor.Components.Common;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

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
