using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Boozor.Shared.Authentication;

public record Signup : IValidatableObject
{
    [EmailAddress]
    [Required]
    public string? Email { get; set; }

    [Required]
    public string? Name { get; set; }

    [Required]
    public string? Hash { get; set; }
    public string? Id { get; set; }

    [Required]
    [MinLength(6)]
    [MaxLength(255)]
    [PasswordPropertyText]
    public string? Password { get; set; }

    [Required]
    [MinLength(6)]
    [MaxLength(255)]
    [PasswordPropertyText]
    [Display(Name = "Retype Password")]
    public string? PasswordRetype { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (!Password!.Equals(PasswordRetype))
            yield return this.NewValidation(x => x.Password, "Password do not match");
    }
}
