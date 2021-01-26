using DevPack.Data.Core;

namespace DevPack.AspNetCore.Mvc
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
