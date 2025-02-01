using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Limupa.Catalog.Api.Entities
{
    public class Feature
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string FeatureID { get; set; }
        public string FeatureTitle { get; set; }
        public string FeatureIcon { get; set; }
        public string FeatureDescription { get; set; }
    }
}
