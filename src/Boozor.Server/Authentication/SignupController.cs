using Boozor.Data;
using Boozor.Shared.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Boozor.Server.Authentication;


[ApiController]
[Route("api/signup")]
public class SignupController : ControllerBase
{
    private readonly IPasswordHasher<Signup> hasher;
    private readonly IRepository<User> repository;

    public SignupController(IPasswordHasher<Signup> hasher, IRepository<User> repository)
    {
        this.hasher = hasher;
        this.repository = repository;
    }

    [HttpPost]
    public async Task<IActionResult> Post(Signup signup, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return ValidationProblem();

        User user = CreateUser(signup);
        var result = user.Validate();

        if (result.Invalid)
            return BadRequest();

        await repository.CreateAsync(user, cancellationToken);
        
        return Created(string.Empty, new { id = user.Id });
    }

    private User CreateUser(Signup signup)
    {
        return new()
        {
            Email = signup.Email,
            Name = signup.Name,
            Hash = hasher.HashPassword(signup, signup.Password!),
        };
    }
}
