namespace Boozor.Business
{
    public static class ValueObjectExtensions
    {
        public static bool IsValid(IValueObject valueObject)
        {
            return !valueObject.GetErrors().Any();
        }

        public static IEnumerable<Result> GetErrors(this IValueObject valueObject)
        {
            return valueObject.Results.Where(x => x is Error);
        }
    }

    public abstract record ValueObject<T> : IValueObject
    {
        public IReadOnlyCollection<Result> Results => _results.ToList();
        private IEnumerable<Result>? _results;

        private readonly List<Func<ValueObject<T>, Result?>> _validations = new();

        private T? _value;

        public T? Value
        {
            get => _value;
            set
            {
                if ((value is not null && !value.Equals(_value)) ||
                    value is null && _value is not null)
                {
                    _value = value;
                    Validate();
                }
            }
        }

        public void AddValidation(Func<ValueObject<T>, Result?> validation)
        {
            _validations.Add(validation);
        }

        public object? GetValue()
        {
            return _value;
        }

        public void Validate()
        {
            _results = getValidations();

            IEnumerable<Result> getValidations()
            {
                foreach (var validate in _validations)
                {
                    var result = validate(this);

                    if (result is not null)
                        yield return result;
                }
            }
        }
    }
}