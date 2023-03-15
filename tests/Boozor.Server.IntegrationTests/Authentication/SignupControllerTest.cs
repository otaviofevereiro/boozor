using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;
using Microsoft.Extensions.DependencyInjection;
using Boozor.Data;
using Boozor.Shared.Authentication;

namespace Boozor.Server.IntegrationTests.Authentication;

public class SignupControllerTest : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> factory;

    public SignupControllerTest(WebApplicationFactory<Program> factory)
    {
        this.factory = factory;
    }

    [Fact]
    public async Task Post_EndpointsReturnSuccessAndCorrectUser()
    {
        // Arrange
        var client = factory.CreateClient();
        var repository = factory.Services.GetRequiredService<IRepository<User>>();
        var signup = CreateValidSignUp();

        //Action
        HttpResponseMessage response = await SignupAsync(client, signup);

        // Assert
        response.EnsureSuccessStatusCode();

        var id = response.GetId();
        var insertedUser = await repository.GetAsync(id);

        Assert.NotNull(insertedUser);
        Assert.Equal(signup.Email, insertedUser.Email);
        Assert.Equal(signup.Name, insertedUser.Name);
        Assert.Equal(id, insertedUser.Id);
    }

    private static Task<HttpResponseMessage> SignupAsync(HttpClient client, Signup signup)
    {
        HttpRequestMessage request = new();
        request.RequestUri = new Uri(client.BaseAddress!, "api/signup");
        request.Method = HttpMethod.Post;
        request.Content = JsonContent.Create(signup);

        return client.SendAsync(request);
    }

    private static Signup CreateValidSignUp()
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
