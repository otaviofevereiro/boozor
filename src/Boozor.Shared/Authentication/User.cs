using System.ComponentModel.DataAnnotations;

namespace Boozor.Shared.Authentication;

public class User : IEntity
{
    [Required]
    public string? UserName { get; set; }

    [Required]
    public string? Hash { get; set; }
    public string? Id { get; set; }
}
