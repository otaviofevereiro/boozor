using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace Boozor.Core
{
    public class Result : IResult
    {
        private List<Validation> _warnings;
        private List<Validation> _errors;
        private List<Validation> _informations;

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public IReadOnlyCollection<Validation> Warnings { get { return _warnings; } set { _warnings = value.ToList(); } }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public IReadOnlyCollection<Validation> Errors { get { return _errors; } set { _errors = value.ToList(); } }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public IReadOnlyCollection<Validation> Informations { get { return _informations; } set { _informations = value.ToList(); } }

        [JsonIgnore]
        public bool IsInvalid => _errors is not null && _errors.Any();
        public bool IsValid => !IsInvalid;

        [JsonIgnore]
        public bool HasWarnings => _warnings is not null && _warnings.Any();

        [JsonIgnore]
        public bool HasInformations => _informations is not null && _informations.Any();

        [JsonIgnore]
        public bool HasErrors => _errors is not null && _errors.Any();

        public void AddAlert(string alert)
        {
            EnsureList(ref _warnings);

            _warnings.Add(new Validation(alert));
        }

        public void AddError(string error)
        {
            EnsureList(ref _errors);

            _errors.Add(new Validation(error));
        }

        public void AddError(Validation validation)
        {
            EnsureList(ref _errors);

            _errors.Add(validation);
        }

        public void AddInformation(string information)
        {
            _informations.Add(new Validation(information));
        }

        private void EnsureList(ref List<Validation> list)
        {
            if (list == null)
                list = new List<Validation>();
        }
    }
}
