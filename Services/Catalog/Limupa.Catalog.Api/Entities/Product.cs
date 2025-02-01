using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations.Schema;

namespace Limupa.Catalog.Entities
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ProductID { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public string? ProductImageUrl { get; set; }
        public string ProductDescription { get; set; }
        [BsonIgnore]
        public List<ProductImage> Images { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string CategoryID { get; set; }

        [BsonIgnore]
        public Category Category { get; set; }

        [BsonIgnore]
        public IFormFile? Photo { get; set; }
        public string? SavedUrl { get; set; }
        public string? SavedFileName { get; set; }
        public string? ProductUrlSeo { get; set; }

    }
}
