using System;
using System.ComponentModel.DataAnnotations;
using Boozor.Model;

namespace Example.Shared
{
    public class Person : Entity<Person>
    {
        [Key]
        [Required]
        public int Id { get; set; }

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

        protected override void Validate()
        {
            if (Name == "otavio")
                AddError(p => p.Name, "otavio nao pode");
        }
    }
}
