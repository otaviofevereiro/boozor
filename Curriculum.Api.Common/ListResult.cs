using System.Collections.Generic;

namespace Curriculum.Api.Common
{
    public class ListResult<T> : Result
    {
        private readonly List<T> itens = new List<T>();
        public IReadOnlyCollection<T> Itens => itens;

        internal void Add(T item)
        {
            itens.Add(item);
        }
    }
}
