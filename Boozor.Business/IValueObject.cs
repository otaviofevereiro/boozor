namespace Boozor.Business
{
    public interface IValueObject
    {
        public IReadOnlyCollection<Result> Results { get; }
        object? GetValue();
    }
}