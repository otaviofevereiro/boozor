using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Curriculum.Shared
{
    public class Person
    {
        [Required]
        public DateTime? BirthDate { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public int? Id { get; set; }
        [Required]
        [StringLength(10, ErrorMessage = "Name is too long.")]
        [DisplayName("Birth Date")]
        public string Name { get; set; }
    }
}
