using FluentValidation;
using System.Text.Json.Serialization;

namespace Boozor.Core
{
    public interface IValidatableObject
    {
        [JsonIgnore]
        public IValidator Validator { get; }

        public IResult Validate();
    }
}
