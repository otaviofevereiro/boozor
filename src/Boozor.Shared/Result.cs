using System.ComponentModel.DataAnnotations;

namespace System;

public record Result<T>
{
    public Result(bool valid)
    {
        Valid = valid;
    }

    public Result(bool valid, T content)
    {
        Valid = valid;
        Content = content;
    }

    public bool Valid { get; }
    public bool Invalid => !Valid;
    public T? Content { get; }
}

public static class ResultExtensions
{
    public static async Task<Result<TContent>> WhenValidAsync<TContent>(this bool valid, Func<Task<TContent>> task)
    {
        if (valid)
            return new Result<TContent>(valid, await task.Invoke());

        return new Result<TContent>(valid);
    }

    public static Result<ICollection<ValidationResult>> FromResult(this ICollection<ValidationResult>? validations)
    {
        if (validations is not null)
            return new(!validations.Any(), validations.ToArray());

        return new(false, Array.Empty<ValidationResult>());
    }
}
