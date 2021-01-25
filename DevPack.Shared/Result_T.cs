namespace DevPack.Shared
{
    public class Result<T> : Result, IResultObject
    {
        public Result()
        {
        }

        public Result(T item)
        {
            Item = item;
        }

        public T Item { get; set; }

        public object GetItem()
        {
            return Item;
        }
    }
}
