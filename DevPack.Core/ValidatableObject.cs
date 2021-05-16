using FluentValidation;
using System;
using System.Text.Json.Serialization;

namespace DevPack.Data.Core
{
    public abstract class ValidatableObject<T> : IValidatableObject
        where T : ValidatableObject<T>
    {
        private readonly Lazy<Validator<T>> validatorLazy;

        protected ValidatableObject()
        {
            validatorLazy = new Lazy<Validator<T>>(() => ConfigureInternal());
        }

        [JsonIgnore]
        public Validator<T> Validator => validatorLazy.Value;

        IValidator IValidatableObject.Validator => Validator;

        public static Validator<T> CreateValidator()
        {
            return Activator.CreateInstance<T>().Validator;
        }

        public IResult Validate()
        {
            var fluentResult = ((IValidator)Validator).Validate(this);
            var result = new Result();

            foreach (var errors in fluentResult.Errors)
                result.AddError(new Validation(errors.PropertyName, errors.ErrorMessage));

            return result;
        }

        protected abstract void Configure(Validator<T> validator);

        private Validator<T> ConfigureInternal()
        {
            var validator = new Validator<T>();

            Configure(validator);

            return validator;
        }
    }
}
