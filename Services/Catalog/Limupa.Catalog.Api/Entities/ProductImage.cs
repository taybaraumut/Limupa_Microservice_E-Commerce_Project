using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Limupa.Catalog.Entities
{
    public class ProductImage
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ProductImageID { get; set; }
        public List<string> ProductBigImageUrl { get; set; }
        public List<string> ProductSmallImageUrl { get; set; }
        public string ProductID { get; set; }
        public string ProductUrlSeo { get; set; }      
        [BsonIgnore]
        public Product Product { get; set; }

        public List<string>? BigSavedUrl { get; set; }
        public List<string>? BigSavedFileName { get; set; }
        public List<string>? SmallSavedUrl { get; set; }
        public List<string>? SmallSavedFileName { get; set; }

        [BsonIgnore]
        public List<IFormFile>? BigPhoto { get; set; }
        [BsonIgnore]
        public List<IFormFile>? SmallPhoto { get; set; }

    }
}
