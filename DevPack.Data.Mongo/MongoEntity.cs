using DevPack.Data.Core;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DevPack.Data.Mongo
{
    public class MongoEntity : Entity<string>
    {
        [BsonId]
        public override string Id { get; set; }

        public static string CreateId()
        {
            return ObjectId.GenerateNewId().ToString();
        }
    }
}
