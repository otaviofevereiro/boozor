namespace Boozor.Core
{
    public interface IResultObject : IResult
    {
        public object GetItem();
    }
}