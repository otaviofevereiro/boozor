using System;
using System.Text.Json;

namespace DevPack.Data.Core
{
    public abstract class Entity<TEntity, TId> : ValidatableObject<TEntity>, ICloneable
        where TEntity : Entity<TEntity, TId>
    {
        protected Entity()
        {
        }

        public TId Id { get; set; }

        public object Clone()
        {
            return JsonSerializer.Deserialize(JsonSerializer.Serialize(this), this.GetType());
        }
    }
}
