using FluentValidation;
using System.Text.Json.Serialization;

namespace DevPack.Data.Core
{
    public interface IValidatableObject
    {
        [JsonIgnore]
        public IValidator Validator { get; }

        public IResult Validate();
    }
}
