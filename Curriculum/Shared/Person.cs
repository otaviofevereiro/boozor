using System;
using System.ComponentModel.DataAnnotations;

namespace Curriculum.Shared
{

    public class Person : Entity
    {
        [Required]
        [Display(Name = "Birth Date")]
        public DateTime? BirthDate { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(60, ErrorMessage = "Name is too long.")]
        public string Name { get; set; }
    }
}
