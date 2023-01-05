using System.Collections.Generic;
using System;
using MongoDB.Bson.Serialization.Attributes;

namespace ContactMicroService.Entities
{
    public class ContactDetail
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string uuid { get; set; }
        public List<ContactInformation> ContactInformation { get; set; }
    }
}
