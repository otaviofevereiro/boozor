using Boozor.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Boozor.Server.Authentication;


[ApiController]
[Route("api/user")]
public class UserController : ControllerBase
{
    private readonly IPasswordHasher<Login> hasher;
    private readonly IRepository<User> repository;

    public UserController(IPasswordHasher<Login> hasher, IRepository<User> repository)
    {
        this.hasher = hasher;
        this.repository = repository;
    }

    [HttpPost]
    public async Task<IActionResult> Post(Login login, CancellationToken cancellationToken)
    {
        User user = new()
        {
            UserName = login.User,
            Hash = hasher.HashPassword(login, login.Password!)
        };

        await repository.CreateAsync(user, cancellationToken);

        return CreatedAtAction(nameof(Get), new { id = user.Id }, new { id = user.Id });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(string id)
    {
        var user = await repository.GetAsync(id);

        if (user is null)
            return NotFound(new { Id = id });

        return Ok(new Login() { User = user.UserName });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(string id, Login login, CancellationToken cancellationToken)
    {
        User user = new()
        {
            UserName = login.User,
            Hash = hasher.HashPassword(login, login.Password!)
        };

        await repository.UpdateAsync(user, cancellationToken);

        return Ok();
    }

    [HttpPut("login/{id}")]
    public async Task<IActionResult> Login(Login login)
    {
        var user = await repository.GetAsync(login.Id);

        if (user is null)
            return Forbid();

        var result = hasher.VerifyHashedPassword(login, user.Hash, login.Password);

        if (result == PasswordVerificationResult.Success)
            return Ok();
        else
            return BadRequest();
    }
}
