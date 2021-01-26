using System.Collections.Generic;
using System.Linq;

namespace DevPack.Data.Core
{
    public class Result : IResult
    {
        private List<Validation> warnings = new List<Validation>();
        private List<Validation> errors = new List<Validation>();
        private List<Validation> informations = new List<Validation>();

        public IReadOnlyCollection<Validation> Warnings { get { return warnings; } set { warnings = value.ToList(); } }
        public IReadOnlyCollection<Validation> Errors { get { return errors; } set { errors = value.ToList(); } }
        public IReadOnlyCollection<Validation> Informations { get { return informations; } set { informations = value.ToList(); } }

        public bool IsInvalid => errors.Any();
        public bool IsValid => !IsInvalid;

        public bool HasWarnings => warnings.Any();
        public bool HasInformations => informations.Any();
        public bool HasErrors => errors.Any();

        public void AddAlert(string alert)
        {
            warnings.Add(new Validation(alert));
        }

        public void AddError(string error)
        {
            errors.Add(new Validation(error));
        }

        public void AddError(Validation validation)
        {
            errors.Add(validation);
        }

        public void AddInformation(string information)
        {
            informations.Add(new Validation(information));
        }
    }
}
