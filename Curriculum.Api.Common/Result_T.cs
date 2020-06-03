namespace Curriculum.Api.Common
{
    public class Result<T> : Result
    {
        public Result()
        {
        }

        public Result(T item)
        {
            Item = item;
        }

        public T Item { get; set; }
    }
}
