using System;
using System.ComponentModel.DataAnnotations;

namespace Curriculum.Shared
{
    public class Experience : Entity
    {
        [Required]
        [Display(Name = "Company Name")]
        [StringLength(60, ErrorMessage = "Company Name is too long.")]
        public string CompanyName { get; set; }

        [Required]
        [Display(Name = "Current Job")]
        public bool CurrentJob { get; set; }

        [Required]
        [StringLength(500, ErrorMessage = "Description is too long.")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Employment Type")]
        public EmploymentType EmploymentType { get; set; }

        [Display(Name = "End Date")]
        public DateTime? EndDate { get; set; }

        [Required]
        [Display(Name = "Start Date")]
        public DateTime? StartDate { get; set; }

        [Required]
        [StringLength(60, ErrorMessage = "Title Name is too long.")]
        public string Title { get; set; }
    }
}
