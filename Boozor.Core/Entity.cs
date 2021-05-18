using System;
using System.Text.Json;

namespace Boozor.Core
{
    public abstract class Entity<TEntity> : ValidatableObject<TEntity>, ICloneable
        where TEntity : Entity<TEntity>
    {
        protected Entity()
        {
        }

        public string Id { get; set; }

        public object Clone()
        {
            return JsonSerializer.Deserialize(JsonSerializer.Serialize(this), this.GetType());
        }
    }
}
