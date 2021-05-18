using FluentValidation;
using System;
using System.ComponentModel.DataAnnotations;
using Boozor.Core;

namespace Curriculum.Shared
{
    public class Person : Entity<Person>
    {
        [Display(Name = "Birth Date")]
        public DateTime? BirthDate { get; set; }

        [Display(Name = "Current Email")]

        public string Email { get; set; }

        public string Name { get; set; }

        public override string ToString()
        {
            return $"{BirthDate} - {Email} - {Name}";
        }

        protected override void Configure(Validator<Person> validator)
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
    }
}
