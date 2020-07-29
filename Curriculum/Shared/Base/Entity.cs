using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Curriculum.Shared.Base
{
    public abstract class Entity : ICloneable
    {
        public int? Id { get; set; }


        public object Clone()
        {
            if (this.GetType().IsSerializable)
                throw new ArgumentException("The type must be serializable.");

            var formatter = new BinaryFormatter();

            using (var stream = new MemoryStream())
            {
                formatter.Serialize(stream, this);
                stream.Seek(0, SeekOrigin.Begin);

                return formatter.Deserialize(stream);
            }
        }
    }
}
