using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace ContactMicroService.Entities
{
    public class Contact
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string uuid { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string Firma { get; set; }
        public List<ContactInformation> ContactInformation { get; set; }

    }

    public class ContactInformation
    {
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Location { get; set; }
    }
}
