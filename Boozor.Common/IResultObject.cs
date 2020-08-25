namespace Boozor.Common
{
    public interface IResultObject : IResult
    {
        public object GetItem();
    }
}