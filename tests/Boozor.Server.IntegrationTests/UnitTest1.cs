using Example.Shared;
using Example.Server;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;
using System.Text.Json;

namespace Boozor.Server.IntegrationTests;

public class UnitTest1 : IClassFixture<WebApplicationFactory<Program>>
{
    private static readonly JsonSerializerOptions options = new() { PropertyNameCaseInsensitive = true };

    private readonly WebApplicationFactory<Program> _factory;

    public UnitTest1(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task Get_EndpointsReturnSuccessAndCorrectContentType()
    {
        // Arrange
        var client = _factory.CreateClient();

        Person person = new()
        {
            BirthDate = new DateTime(1990, 01, 01),
            Email = "teste@teste.com",
            Name = "teste"
        };

        HttpRequestMessage request = new();
        request.RequestUri = new Uri(client.BaseAddress!, "api/entities");
        request.Method = HttpMethod.Post;
        request.Headers.Add("type", typeof(Person).FullName);
        request.Content = JsonContent.Create(person);

        // Act
        var response = await client.SendAsync(request);

        // Assert
        response.EnsureSuccessStatusCode();
        var stream = await response.Content.ReadAsStreamAsync();
        var content = JsonSerializer.Deserialize<JsonElement>(stream, options);


        Assert.NotNull(content);

        var id = content.GetProperty("id").GetString();

        Assert.NotNull(id);
        Assert.NotEmpty(id);
        Assert.True(Guid.TryParse(id, out Guid _));
    }
}