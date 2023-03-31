using System.Security.Claims;
using Boozor.Data;
using Boozor.Shared.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Boozor.Server.Authentication;

[Route("api/auth")]
public class AuthController : Controller
{
    private readonly IPasswordHasher<Signin> hasher;
    private readonly IRepository<User> repository;

    public AuthController(IPasswordHasher<Signin> hasher, IRepository<User> repository)
    {
        this.hasher = hasher;
        this.repository = repository;
    }

    [HttpPost("signin")]
    public async Task<IActionResult> SigninAsync([FromBody]Signin signin)
    {
        if (!ModelState.IsValid)
            return ValidationProblem();

        var user = await repository.GetAsync(x => x.Email == signin.Email);

        if (user is null)
            return BadRequest("Invalid email or password");

        var result = hasher.VerifyHashedPassword(signin, user.Hash!, signin.Password!);

        if (result != PasswordVerificationResult.Success)
            return BadRequest("Invalid email or password");

        var claims = GetClaims(user);
        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var authProperties = GetAuthenticationProperties();

        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity),
            authProperties);

        return Ok();
    }

    [Authorize]
    [HttpPut("signout")]
    public async Task SignOutAsync()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    }

    private static IEnumerable<Claim> GetClaims(User user)
    {
        yield return new Claim(ClaimTypes.Email, user.Email!);
        yield return new Claim(ClaimTypes.Name, user.Name!);
        yield return new Claim(ClaimTypes.Role, "user"); //TODO: get roles
    }

    private static AuthenticationProperties GetAuthenticationProperties()
    {
        return new()
        {
            ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
            IssuedUtc = DateTimeOffset.UtcNow,
        };
    }
}
