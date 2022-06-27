namespace Boozor.Data.Objects
{
    public record RawValue<T>
    {
        public RawValue(T value)
        {
            Value = value;
        }

        public T Value { get; }

        public object? GetValue()
        {
            return Value;
        }
    }
}