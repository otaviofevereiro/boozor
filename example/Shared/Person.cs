using System;
using System.ComponentModel.DataAnnotations;
using Boozor.Shared;

namespace Example.Shared
{
    [Title("Person Entity")]
    public class Person : IEntity
    {
        [Display(Name = "Birth Date")]
        [Required]
        public DateTime? BirthDate { get; set; } = DateTime.Now;

        [Display(Name = "Current Email")]
        [Required]
        [EmailAddress]
        public string? Email { get; set; } = "a@a.com.br";

        [Required]
        [StringLength(60)]
        public string? Name { get; set; } = "Teste";


        public string? Id { get; set; }

        public bool Active { get; set; }
        public decimal? Value { get; set; }


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Name == "otavio")
                yield return this.NewValidation(p => p.Name, "otavio nao pode");
        }
    }
}
