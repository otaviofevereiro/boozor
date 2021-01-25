using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.Json;

namespace DevPack.Data.Core
{
    public abstract class Entity<TId> : ICloneable
    {
        public virtual TId Id { get; set; }

        public object Clone()
        {
            return JsonSerializer.Deserialize(JsonSerializer.Serialize(this), this.GetType());
        }
    }
}
