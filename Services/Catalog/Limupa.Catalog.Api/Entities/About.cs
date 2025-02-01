using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Limupa.Catalog.Api.Entities
{
    public class About
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string AboutID { get; set; }
        public string AboutDescription { get; set; }
        public string AboutAddress { get; set; }
        public string AboutEmail { get; set; }
        public string AboutPhone { get; set; }
    }
}
