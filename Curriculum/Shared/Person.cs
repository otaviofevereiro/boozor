﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Curriculum.Shared
{
    public class Person
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
        public string Email { get; set; }

        [Required]
        [StringLength(60)]
        public string Name { get; set; }

    }
}
