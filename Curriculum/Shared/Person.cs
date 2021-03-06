﻿using Boozor.Common;
using FluentValidation;
using System;
using System.ComponentModel.DataAnnotations;

namespace Curriculum.Shared
{
    public class Person : Entity<Person>
    {
        [Display(Name = "Birth Date")]
        public DateTime? BirthDate { get; set; }

        [Display(Name = "Current Email")]

        public string Email { get; set; }

        public string Name { get; set; }

        protected override void Configure(EntityValidator<Person> validator)
        {
            validator.RuleFor(x => x.BirthDate)
                     .NotEmpty();

            validator.RuleFor(x => x.Email)
                     .NotEmpty()
                     .MaximumLength(250)
                     .EmailAddress();

            validator.RuleFor(x => x.Name)
                     .NotEmpty()
                     .MaximumLength(250);
        }

        public override string ToString()
        {
            return $"{BirthDate} - {Email} - {Name}";
        }
    }
}
