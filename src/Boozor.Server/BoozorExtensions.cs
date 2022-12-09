using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Boozor.Server;

namespace Boozor;

public static class BoozorExtensions
{
    public static IServiceCollection AddBoozor<TModelAssembly>(this IServiceCollection services)
    {
        var modelAssembly = Assembly.GetAssembly(typeof(TModelAssembly)) ?? throw new InvalidOperationException("Assembly not found.");

        services.AddControllersWithViews()
                .AddApplicationPart(modelAssembly);

        BoozorContext boozorContext = new(modelAssembly);

        return services.AddSingleton(boozorContext);
    }
}
