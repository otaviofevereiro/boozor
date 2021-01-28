

namespace DevPack.Data.Core
{
    public class EntityResult<TId>
    {
        public EntityResult(TId id, IResult result)
        {
            Id = id;
            Result = result;
        }

        public TId Id { get; }
        public IResult Result { get; }
    }
}
