using System.Collections.Generic;
using System.Linq;

namespace Curriculum.Api.Common
{
    public class Result 
    {
        private readonly List<string> errors = new List<string>();

        public IReadOnlyCollection<string> Errors => errors;
        public bool IsInvalid => errors.Any();
        public bool IsValid => !IsInvalid;

        internal void AddError(string error)
        {
            errors.Add(error);
        }
    }
}
