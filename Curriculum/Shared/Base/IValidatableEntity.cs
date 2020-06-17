using FluentValidation;

namespace Curriculum.Shared.Base
{
    public interface IValidatableEntity
    {
        public IValidator GetValidator();
    }
}
