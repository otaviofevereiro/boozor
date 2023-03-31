using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;
using Microsoft.Extensions.DependencyInjection;
using Boozor.Data;
using Boozor.Shared.Authentication;

namespace Boozor.Server.IntegrationTests.Authentication;

public class AuthControllerTest : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> factory;

    public AuthControllerTest(WebApplicationFactory<Program> factory)
    {
        this.factory = factory;
    }

    [Fact]
    public async Task Put_EndpointsReturnSuccess()
    {
        // Arrange
        var client = factory.CreateClient();
        var repository = factory.Services.GetRequiredService<IRepository<User>>();
        var signup = await SignupAsync(client);
        var signin = CreateValidSignin(signup);

        //Action
        HttpResponseMessage response = await SigninAsync(client, signin);

        // Assert
        response.EnsureSuccessStatusCode();

        var signoutResponse = await SignoutAsync(client);
        signoutResponse.EnsureSuccessStatusCode();
    }

    private static Task<HttpResponseMessage> SigninAsync(HttpClient client, Signin login)
    {
        HttpRequestMessage request = new();
        request.RequestUri = new Uri(client.BaseAddress!, "api/auth/signin");
        request.Method = HttpMethod.Post;
        request.Content = JsonContent.Create(login);

        return client.SendAsync(request);
    }

    private static Task<HttpResponseMessage> SignoutAsync(HttpClient client)
    {
        HttpRequestMessage request = new();
        request.RequestUri = new Uri(client.BaseAddress!, "api/auth/signout");
        request.Method = HttpMethod.Put;

        return client.SendAsync(request);
    }

    private static async Task<Signup> SignupAsync(HttpClient client)
    {
        Signup signup = CreateValidSignup();

        HttpRequestMessage request = new();
        request.RequestUri = new Uri(client.BaseAddress!, "api/signup");
        request.Method = HttpMethod.Post;
        request.Content = JsonContent.Create(signup);

        var response = await client.SendAsync(request);
        response.EnsureSuccessStatusCode();

        return signup;
    }

    private static Signin CreateValidSignin(Signup signup)
    {
        return new()
        {
            Email = signup.Email,
            Password = signup.Password,
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
