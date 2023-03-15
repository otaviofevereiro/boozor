using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Boozor.Server;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Boozor;

public static class BoozorExtensions
{
    private static CookiePolicyOptions cookiePolicyOptions = new()
    {
        MinimumSameSitePolicy = SameSiteMode.Strict,
    };

    public static IServiceCollection AddBoozor<TModelAssembly>(this IServiceCollection services)
    {
        var modelAssembly = Assembly.GetAssembly(typeof(TModelAssembly)) ?? throw new InvalidOperationException("Assembly not found.");

        services.AddControllersWithViews()
                .AddApplicationPart(modelAssembly);

        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
                    options.SlidingExpiration = true;
                    options.AccessDeniedPath = "/Forbidden/";
                });

        BoozorContext boozorContext = new(modelAssembly);

        return services.AddSingleton(boozorContext)
                       .AddTransient(typeof(IPasswordHasher<>), typeof(PasswordHasher<>));


    }

    public static IApplicationBuilder UseBoozor(this IApplicationBuilder app)
    {
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseCookiePolicy(cookiePolicyOptions);

        return app;
    }
}
