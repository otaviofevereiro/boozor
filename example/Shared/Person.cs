using System;
using System.ComponentModel.DataAnnotations;
using Boozor.Shared;

namespace Example.Shared
{
    public class Person : IEntity
    {
        public static string EntityName => nameof(Person);

        [Display(Name = "Birth Date")]
        [Required]
        public DateTime? BirthDate { get; set; }

        [Display(Name = "Current Email")]
        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        [StringLength(60)]
        public string? Name { get; set; }


        public string? Id { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Name == "otavio")
                yield return this.NewValidation(p => p.Name, "otavio nao pode");
        }
    }
}
