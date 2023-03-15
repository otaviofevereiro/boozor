using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Boozor.Shared;

namespace Boozor.Shared.Authentication;

public record Signin
{
    [Required]
    public string? Email { get; set; }

    [Required]
    [MinLength(6)]
    [MaxLength(255)]
    [PasswordPropertyText]
    public string? Password { get; set; }
}
