using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Boozor.Api;

public static class Boozor
{
    public static IServiceCollection AddBoozor<TModelAssembly>(this IServiceCollection services)
    {
        var modelAssembly = Assembly.GetAssembly(typeof(TModelAssembly)) ?? throw new InvalidOperationException("Assembly not found.");

        services.AddControllersWithViews()
                .AddApplicationPart(modelAssembly);

        BoozorContext boozorContext = new(modelAssembly);

        services.AddSingleton(boozorContext);

        return services;
    }
}
