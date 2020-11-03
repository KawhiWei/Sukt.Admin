using MongoDB.Bson;
using Sukt.Core.Shared.Entity;

namespace Sukt.Core.MongoDB
{
    public abstract class MongoEntity : IEntity<ObjectId>
    {
        public ObjectId Id { get; set; } = ObjectId.GenerateNewId();
    }
}