using System.Reflection;
using Example.Shared;
using Microsoft.AspNetCore.ResponseCompression;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddBoozor<Person>();
//builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UsePathBase("/api");

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();


//app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();

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

public sealed class BoozorContext
{
    private readonly Assembly _modelAssembly;

    public BoozorContext(Assembly modelAssembly)
    {
        _modelAssembly = modelAssembly;
    }

    public Type GetModelType(string typeName)
    {
        return _modelAssembly.GetType(typeName) ?? throw new InvalidOperationException($"Type {typeName} not found."); ;
    }
}