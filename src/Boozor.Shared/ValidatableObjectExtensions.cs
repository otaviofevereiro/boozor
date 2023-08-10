using System.ComponentModel.DataAnnotations;

namespace System;

public static class ValidatableObjectExtensions
{
    public static Result<ICollection<ValidationResult>> Validate<T>(this T instance)
     where T : IValidatableObject
    {
        ValidationContext validationContext = new(instance);
        var validations = Array.Empty<ValidationResult>();
        Validator.TryValidateObject(instance, validationContext, validations);

        return validations.FromResult();
    }
}