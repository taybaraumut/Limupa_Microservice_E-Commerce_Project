using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Limupa.Catalog.Api.Entities
{
    public class SpecialOffer
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string SpecialOfferID { get; set; }
        public string SpecialOfferImageUrl { get; set; }
    }
}
