using System.ComponentModel;

namespace Curriculum.Shared
{
    public enum EmploymentType
    {
        [Description("Full Time")]
        FullTime = 1,
        [Description("Part Time")]
        PartTime = 2,
        [Description("Self Employed")]
        SelfEmployed = 3,
        Freelance = 4,
        Contract = 5,
        Intership = 6,
        Apprenticeship = 7
    }
}
