using System.ComponentModel.DataAnnotations;
using Boozor.Shared;

namespace Boozor.Server.Authentication;

public record Login : IEntity
{
    [Required]
    public string? User { get; set; }

    [Required]
    public string? Password { get; set; }

    public string? Id { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        yield break;
    }
}
