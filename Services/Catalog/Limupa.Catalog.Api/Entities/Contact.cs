using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Limupa.Catalog.Api.Entities
{
    public class Contact
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ContactID { get; set; }
        public string ContactName { get; set; }
        public string ContactEmail { get; set; }
        public string ContactSubject { get; set; }
        public string ContactMessage { get; set; }
        public bool ContactIsRead { get; set; }
        public DateTime ContactSendDate { get; set; }
    }
}
