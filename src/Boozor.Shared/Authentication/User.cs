using System.ComponentModel.DataAnnotations;

namespace Boozor.Shared.Authentication;

public class User : IEntity
{
    [Required]
    [EmailAddress]
    public string? Email { get; set; }

    [Required]
    public string? Name { get; set; }

    [Required]
    public string? Hash { get; set; }

    public string? Id { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        yield break;
    }
}
