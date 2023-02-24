using System.ComponentModel.DataAnnotations;
using Boozor.Shared;

namespace Boozor.Shared.Authentication;

public record Login 
{
    [Required]
    public string? Email { get; set; }

    [Required]
    public string? Password { get; set; }
}
