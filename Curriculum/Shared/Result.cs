using System.Collections.Generic;
using System.Linq;

namespace Curriculum.Shared
{
    public class Result
    {
        private readonly List<string> errors = new List<string>();

        public IReadOnlyCollection<string> Errors => errors;
        public bool IsInvalid => errors.Any();
        public bool IsValid => !IsInvalid;

        public void AddError(string error)
        {
            errors.Add(error);
        }
    }

    public class Result<T> : Result
    {
        public Result(T item)
        {
            Item = item;
        }

        public T Item { get; }
    }
}
