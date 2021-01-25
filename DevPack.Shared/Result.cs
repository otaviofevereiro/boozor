using System.Collections.Generic;
using System.Linq;

namespace DevPack.Shared
{
    public class Result : IResult
    {
        private List<string> warnings = new List<string>();
        private List<string> errors = new List<string>();
        private List<string> informations = new List<string>();

        public IReadOnlyCollection<string> Warnings { get { return warnings; } set { warnings = value.ToList(); } }
        public IReadOnlyCollection<string> Errors { get { return errors; } set { errors = value.ToList(); } }
        public IReadOnlyCollection<string> Informations { get { return informations; } set { informations = value.ToList(); } }

        public bool IsInvalid => errors.Any();
        public bool IsValid => !IsInvalid;

        public bool HasWarnings => warnings.Any();
        public bool HasInformations => informations.Any();
        public bool HasErrors => errors.Any();

        public void AddAlert(string alert)
        {
            warnings.Add(alert);
        }

        public void AddError(string error)
        {
            errors.Add(error);
        }

        public void AddInformation(string information)
        {
            informations.Add(information);
        }
    }
}
