namespace DevPack.Data.Core
{
    public class Validation
    {
        public Validation(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
                throw new System.ArgumentException($"'{nameof(message)}' cannot be null or whitespace", nameof(message));

            Message = message;
        }

        public Validation(string key, string message)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new System.ArgumentException($"'{nameof(key)}' cannot be null or whitespace", nameof(key));

            if (string.IsNullOrWhiteSpace(message))
                throw new System.ArgumentException($"'{nameof(message)}' cannot be null or whitespace", nameof(message));

            Key = key;
            Message = message;
        }

        public string Key { get; }
        public string Message { get; }
    }
}
