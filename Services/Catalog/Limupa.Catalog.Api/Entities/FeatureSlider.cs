using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Limupa.Catalog.Api.Entities
{
    public class FeatureSlider
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string FeatureSliderID { get; set; }
        public string FeatureSliderTitle { get; set; }
        public string FeatureSliderImageUrl { get; set; }
        public string FeatureSliderStatus { get; set; }
    }
}
