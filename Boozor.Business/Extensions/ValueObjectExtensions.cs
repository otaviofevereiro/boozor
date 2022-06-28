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
            return valueObject.GetResults()
                              .Where(x => x is Error);
        }
    }
}