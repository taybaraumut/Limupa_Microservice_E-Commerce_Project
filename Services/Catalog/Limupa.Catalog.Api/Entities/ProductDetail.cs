﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Limupa.Catalog.Entities
{
    public class ProductDetail
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ProductDetailID { get; set; }
        public string ProductDescription { get; set; }
        public string ProductInfo { get; set; }
        public string ProductUrlSeo { get; set; }
        public string ProductID { get; set; }
        [BsonIgnore]
        public Product Product { get; set; }

    }
}
