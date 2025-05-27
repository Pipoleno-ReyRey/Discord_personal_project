using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Servers_service.Models
{
    public class UserServer
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? id { get; set; }
        public int? userId { get; set; }
        public string? user { get; set; }
        public string? role { get; set; }
        public string? serverId { get; set; }
    }
}
