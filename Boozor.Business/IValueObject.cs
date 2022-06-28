namespace Boozor.Business
{
    public interface IValueObject
    {
        IEnumerable<Result> GetResults();
        object? GetValue();
    }
}