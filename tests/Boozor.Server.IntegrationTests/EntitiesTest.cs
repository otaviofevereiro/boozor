using Example.Shared;
using Example.Server;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using Boozor.Data;

namespace Boozor.Server.IntegrationTests;

public class EntitiesTest : IClassFixture<WebApplicationFactory<Program>>
{
    private static readonly JsonSerializerOptions options = new() { PropertyNameCaseInsensitive = true };

    private readonly WebApplicationFactory<Program> factory;

    public EntitiesTest(WebApplicationFactory<Program> factory)
    {
        this.factory = factory;
    }

    [Fact]
    public async Task Post_EndpointsReturnSuccessAndCorrectEntity()
    {
        // Arrange
        var client = factory.CreateClient();
        var repository = factory.Services.GetRequiredService<IRepository>();
        var personType = typeof(Person);
        var person = CreateValidPerson();

        //Action
        HttpResponseMessage response = await PostAsync(client, personType, person);

        // Assert
        response.EnsureSuccessStatusCode();
        
        var id = GetId(response);
        var insertedPerson = (await repository.GetAsync(personType, id)) as Person;

        Assert.NotNull(insertedPerson);
        Assert.Equal(person.BirthDate, insertedPerson.BirthDate);
        Assert.Equal(person.Name, insertedPerson.Name);
        Assert.Equal(person.Email, insertedPerson.Email);
    }

    [Fact]
    public async Task Put_EndpointsReturnSuccessAndCorrectEntity()
    {
        // Arrange
        var client = factory.CreateClient();
        var repository = factory.Services.GetRequiredService<IRepository>();
        var personType = typeof(Person);
        var person = CreateValidPerson();
        var insertResponse = await PostAsync(client, personType, person);

        var id = GetId(insertResponse);
        person.Id = id;
        person.Name = "New Name";

        //Action
        HttpResponseMessage response = await PutAsync(client, personType, person);

        // Assert
        response.EnsureSuccessStatusCode();

        var updatedPerson = (await repository.GetAsync(personType, id)) as Person;

        Assert.NotNull(updatedPerson);
        Assert.Equal(person.BirthDate, updatedPerson.BirthDate);
        Assert.Equal(person.Name, updatedPerson.Name);
        Assert.Equal(person.Email, updatedPerson.Email);

    }

    private static Task<HttpResponseMessage> PutAsync(HttpClient client, Type personType, Person person)
    {
        HttpRequestMessage request = new();
        request.RequestUri = new Uri(client.BaseAddress!, "api/entities");
        request.Method = HttpMethod.Put;
        request.Headers.Add("type", personType.FullName);
        request.Content = JsonContent.Create(person);

        return client.SendAsync(request);
    }

    private static Task<HttpResponseMessage> PostAsync(HttpClient client, Type personType, Person person)
    {
        HttpRequestMessage request = new();
        request.RequestUri = new Uri(client.BaseAddress!, "api/entities");
        request.Method = HttpMethod.Post;
        request.Headers.Add("type", personType.FullName);
        request.Content = JsonContent.Create(person);

        return client.SendAsync(request);
    }


    private static string GetId(HttpResponseMessage response)
    {
        var content = JsonSerializer.Deserialize<JsonElement>(response.Content.ReadAsStream(), options);

        Assert.NotNull(content);

        var id = content.GetProperty("id").GetString();

        Assert.NotNull(id);
        Assert.NotEmpty(id);
        Assert.True(Guid.TryParse(id, out Guid _));

        return id;
    }

    private static Person CreateValidPerson()
    {
        return new()
        {
            BirthDate = new DateTime(1990, 01, 01),
            Email = "teste@teste.com",
            Name = "teste"
        };
    }
}