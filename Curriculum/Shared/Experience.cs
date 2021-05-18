using Boozor.Core;
using FluentValidation;
using System;
using System.ComponentModel.DataAnnotations;

namespace Curriculum.Shared
{
    public class Experience : Entity<Experience>
    {
        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }

        [Display(Name = "Current Job")]
        public bool CurrentJob { get; set; }

        public string Description { get; set; }

        [Display(Name = "Employment Type")]
        public EmploymentType EmploymentType { get; set; }

        [Display(Name = "End Date")]
        public DateTime? EndDate { get; set; }

        [Display(Name = "Start Date")]
        public DateTime? StartDate { get; set; }

        public string Title { get; set; }

        protected override void Configure(Validator<Experience> validator)
        {
            validator.RuleFor(x => x.CompanyName)
                     .NotEmpty()
                     .MaximumLength(60);

            validator.RuleFor(x => x.Description)
                     .NotEmpty()
                     .MaximumLength(500);

            validator.RuleFor(x => x.StartDate)
                     .NotEmpty();

            validator.RuleFor(x => x.Title)
                     .NotEmpty()
                     .MaximumLength(500);

            validator.RuleFor(x => x.EmploymentType)
                     .NotEmpty();
        }
    }
}
