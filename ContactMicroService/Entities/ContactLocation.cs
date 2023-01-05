using MongoDB.Bson.Serialization.Attributes;

namespace ContactMicroService.Entities
{
    public class ContactLocation
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string uuid { get; set; }
        public string Location { get; set; }
    }
}
