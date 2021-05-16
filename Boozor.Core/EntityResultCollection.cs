using System.Collections.Generic;
using System.Linq;

namespace Boozor.Core
{
    public class EntityResultCollection<TId>
    {
        private readonly List<EntityResult<TId>> _entityResults = new();

        public IReadOnlyCollection<EntityResult<TId>> EntitiesResult => _entityResults;

        public bool IsValid => _entityResults.All(x => x.Result.IsValid);

        public void Add(TId entityId, IResult result)
        {
            _entityResults.Add(new EntityResult<TId>(entityId, result));
        }
    }
}
