namespace Curriculum.Api.Common
{
    public interface IResultObject : IResult
    {
        public object GetItem();
    }
}