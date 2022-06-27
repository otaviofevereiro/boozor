namespace Boozor.Business
{
    public abstract class Result
    {
        public readonly static Result? Empty = null;

        protected Result(string message)
        {
            if (string.IsNullOrEmpty(message))
                throw new ArgumentNullException(nameof(message));

            Message = message;
        }

        public string Message { get; }
    }
}