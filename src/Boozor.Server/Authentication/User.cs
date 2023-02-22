using System.ComponentModel.DataAnnotations;
using Boozor.Shared;

namespace Boozor.Server.Authentication;

public class User : IEntity
{
    [Required]
    public string? UserName { get; set; }

    [Required]
    public string? Hash { get; set; }
    public string? Id { get; set; }

}
