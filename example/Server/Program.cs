using Example.Shared;
using Boozor;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddBoozor<Person>()
                .AddInMemoryDatabase();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
    app.MapGet("/debug/routes", (IEnumerable<EndpointDataSource> endpointSources) =>
       string.Join("\n", endpointSources.SelectMany(source => source.Endpoints)));
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseBlazorFrameworkFiles();
app.UseStaticFiles();
app.UseRouting();
app.UseCors(cors => cors.AllowAnyMethod()
                        .AllowAnyHeader()
                        .SetIsOriginAllowed(origin => true)
                        .AllowCredentials());


//app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();

public partial class Program { }

