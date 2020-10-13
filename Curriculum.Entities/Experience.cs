using Curriculum.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Curriculum.Entities
{
    public class Experience : Entity
    {
        public string CompanyName { get; set; }
        public bool CurrentJob { get; set; }
        public string Description { get; set; }
        public EmploymentType EmploymentType { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime StartDate { get; set; }
        public string Title { get; set; }
    }
}
