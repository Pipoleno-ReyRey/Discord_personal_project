using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Channels_service.Models
{
    public class Channel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public int? id { get; set; }
        public string? name { get; set; }
        public string? description { get; set; }
        public string? serverId { get; set; }
        public string? server { get; set; }
    }
}
