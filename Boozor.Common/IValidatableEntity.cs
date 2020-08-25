using FluentValidation;

namespace Boozor.Common
{
    public interface IValidatableEntity
    {
        public IValidator GetValidator();
    }
}
