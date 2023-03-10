using ContactMicroService.Data.Interfaces;
using ContactMicroService.Entities;
using ContactMicroService.Settings;
using MongoDB.Driver;

namespace ContactMicroService.Data
{
    public class ContactContext : IContactContext
    {
        public ContactContext(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionStrings);
            var database = client.GetDatabase(settings.DatabaseName);

            Contacts = database.GetCollection<Contact>(settings.ContactCollectionName);
        }

        public IMongoCollection<Contact> Contacts { get; }
    }
}