namespace Boozor.Business
{
    public abstract record ValueObject<T> : IValueObject
    {
        private readonly List<Func<ValueObject<T>, Result?>> _validations = new();
        private IEnumerable<Result> _results = Array.Empty<Result>();
        private T? _value;

        protected ValueObject(T? value)
        {
            Value = value;
            Validate();
        }

        public T? Value { get; }

        public IEnumerable<Result> GetResults()
        {
            return _results;
        }

        public object? GetValue()
        {
            return _value;
        }

        internal void AddValidation(Func<ValueObject<T>, Result?> validation)
        {
            _validations.Add(validation);
        }

        protected abstract void Rules();

        private void Validate()
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