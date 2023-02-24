using Boozor.Data;
using Boozor.Shared;
using Boozor.Shared.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Boozor.Server.Authentication;


[ApiController]
[Route("api/login")]
public class LoginController : ControllerBase
{
    private readonly IPasswordHasher<Login> hasher;
    private readonly IRepository<User> repository;

    public LoginController(IPasswordHasher<Login> hasher, IRepository<User> repository)
    {
        this.hasher = hasher;
        this.repository = repository;
    }

    [HttpPut]
    public async Task<IActionResult> Put(Login login)
    {
        var user = await repository.GetAsync(x => x.Email == login.Email);

        if (user is null)
            return Forbid();

        var result = hasher.VerifyHashedPassword(login, user.Hash!, login.Password!);

        if (result == PasswordVerificationResult.Success)
            return Ok();
        else
            return BadRequest();
    }
}

public class User : IEntity
{
    public string? Email { get; set; }
    public string? Name { get; set; }
    public string? Hash { get; set; }
    public string? Id { get; set; }
}
