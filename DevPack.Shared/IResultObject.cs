namespace DevPack.Shared
{
    public interface IResultObject : IResult
    {
        public object GetItem();
    }
}