namespace DevPack.Data.Core
{
    public interface IResultObject : IResult
    {
        public object GetItem();
    }
}