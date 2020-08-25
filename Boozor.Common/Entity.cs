using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.Json;

namespace Boozor.Common
{
    public abstract class Entity : ICloneable
    {
        public int? Id { get; set; }

        public object Clone()
        {
            return JsonSerializer.Deserialize(JsonSerializer.Serialize(this), this.GetType());
        }
    }
}
