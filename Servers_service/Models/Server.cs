using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Servers_service.Models
{
    public class Server
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? id { get; set; }
        public string? image { get; set; }
        public string? name { get; set; }
        public string? description { get; set; }
        public string? link { get; set; }
        public DateOnly? creationDate { get; set; }
        public bool? state { get; set; }
        public string? creator { get; set; }
    }
}
