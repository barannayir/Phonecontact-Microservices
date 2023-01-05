using ContactMicroService.Entities;
using MongoDB.Driver;

namespace ContactMicroService.Data.Interfaces
{
    public interface IContactContext
    {
        IMongoCollection<Contact> Contacts { get;  }
    }
}
