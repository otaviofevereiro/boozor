using Example.Shared;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using Boozor.Data;

namespace Boozor.Server.IntegrationTests;

public class EntitiesTest : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> factory;

    public EntitiesTest(WebApplicationFactory<Program> factory)
    {
        this.factory = factory;
    }

    //TODO get
    //TODO delete

    [Fact]
    public async Task Post_EndpointsReturnSuccessAndCorrectEntity()
    {
        // Arrange
        var client = factory.CreateClient();
        var repository = factory.Services.GetRequiredService<IRepository<Person>>();
        var personType = typeof(Person);
        var person = CreateValidPerson();

        //Action
        HttpResponseMessage response = await PostAsync(client, personType, person);

        // Assert
        response.EnsureSuccessStatusCode();

        var id = response.GetId();
        person.Id = id;
        var insertedPerson = await repository.GetAsync(id);

        Assert.NotNull(insertedPerson);
        Assert.Equivalent(person, insertedPerson);
    }

    [Fact]
    public async Task Put_EndpointsReturnSuccessAndCorrectEntity()
    {
        // Arrange
        var client = factory.CreateClient();
        var repository = factory.Services.GetRequiredService<IRepository<Person>>();
        var personType = typeof(Person);
        var person = CreateValidPerson();
        var insertResponse = await PostAsync(client, personType, person);

        var id = insertResponse.GetId();
        person.Id = id;
        person.Name = "New Name";

        //Action
        HttpResponseMessage response = await PutAsync(client, personType, person);

        // Assert
        response.EnsureSuccessStatusCode();

        var updatedPerson = await repository.GetAsync(id);

        Assert.NotNull(updatedPerson);
        Assert.Equivalent(person, updatedPerson);
    }

    [Fact]
    public async Task Delete_EndpointsReturnSuccessAndDeleteEntity()
    {
        // Arrange
        var client = factory.CreateClient();
        var repository = factory.Services.GetRequiredService<IRepository<Person>>();
        var personType = typeof(Person);
        var person = CreateValidPerson();
        var insertResponse = await PostAsync(client, personType, person);

        var id = insertResponse.GetId();

        //Action
        HttpResponseMessage response = await DeleteAsync(client, personType, id);

        // Assert
        response.EnsureSuccessStatusCode();

        var insertedPerson = await repository.GetAsync(id);

        Assert.Null(insertedPerson);
    }

    private static Task<HttpResponseMessage> DeleteAsync(HttpClient client, Type personType, string id)
    {
        HttpRequestMessage request = new();
        request.RequestUri = new Uri(client.BaseAddress!, $"api/entities/{id}");
        request.Method = HttpMethod.Delete;
        request.Headers.Add("type", personType.FullName);

        return client.SendAsync(request);
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