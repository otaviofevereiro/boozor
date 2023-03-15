using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;
using Microsoft.Extensions.DependencyInjection;
using Boozor.Data;
using Boozor.Shared.Authentication;

namespace Boozor.Server.IntegrationTests.Authentication;

public class SigninControllerTest : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> factory;

    public SigninControllerTest(WebApplicationFactory<Program> factory)
    {
        this.factory = factory;
    }

    [Fact]
    public async Task Put_EndpointsReturnSuccessAndTODOOOOOOOOOOOOOOOO()
    {
        // Arrange
        var client = factory.CreateClient();
        var repository = factory.Services.GetRequiredService<IRepository<User>>();
        var login = CreateValidLogin();

        //Action
        HttpResponseMessage response = await LoginAsync(client, login);

        // Assert
        response.EnsureSuccessStatusCode();
    }

    private static Task<HttpResponseMessage> LoginAsync(HttpClient client, Signin login)
    {
        HttpRequestMessage request = new();
        request.RequestUri = new Uri(client.BaseAddress!, "api/login");
        request.Method = HttpMethod.Put;
        request.Content = JsonContent.Create(login);

        return client.SendAsync(request);
    }

    private static Task<HttpResponseMessage> SignupAsync(HttpClient client)
    {
        HttpRequestMessage request = new();
        request.RequestUri = new Uri(client.BaseAddress!, "api/signup");
        request.Method = HttpMethod.Post;
        request.Content = JsonContent.Create(CreateValidSignup());

        return client.SendAsync(request);
    }

    private static Signin CreateValidLogin()
    {
        return new()
        {
            Email = "a@a.com.br",
            Password = "abcd123456789",
        };
    }

    private static Signup CreateValidSignup()
    {
        return new()
        {
            Email = "a@a.com.br",
            Name = "otavio",
            Password = "abcd123456789",
            PasswordRetype = "abcd123456789",
        };
    }
}
