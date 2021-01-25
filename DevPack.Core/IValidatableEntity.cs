using FluentValidation;

namespace DevPack.Data.Core
{
    public interface IValidatableEntity
    {
        public IValidator Validator { get; }
    }
}
