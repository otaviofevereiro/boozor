using System.ComponentModel.DataAnnotations;

namespace System;

public sealed class Result
{
    private readonly ValidationResult[]? _validations;

    public Result(ICollection<ValidationResult>? validations, ValidationContext context)
    {
        if (validations is not null)
        {
            _validations = validations.ToArray();
            Valid = !validations.Any();
        }

        Context = context;
    }

    public ValidationContext Context { get; }

    public bool Invalid => !Valid;
    public bool Valid { get; } = true;
    public IReadOnlyCollection<ValidationResult>? Validations => _validations;
}

public static class ValidatableObjectExtensions
{
    public static Result Validate<T>(this T instance)
     where T : IValidatableObject
    {
        ValidationContext validationContext = new(instance);
        ICollection<ValidationResult>? validations = null;
        Validator.TryValidateObject(instance, validationContext, validations);

        return new(validations, validationContext);
    }
}