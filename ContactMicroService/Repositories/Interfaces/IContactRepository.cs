using ContactMicroService.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContactMicroService.Repositories.Interfaces
{
    public interface IContactRepository
    {
        Task<IEnumerable<Contact>> GetContacts();
        Task<Contact> GetContact(string uuid);
        Task<IEnumerable<Contact>> GetContactsByLocation(string location);
        Task<IEnumerable<Contact>> GetContactsByFirma(string firma);
        Task<IEnumerable<Contact>> GetContactsByAd(string ad);
        Task<IEnumerable<Contact>> GetContactsBySoyad(string soyad);
        Task<Contact> GetContactsByPhoneNumber(string phoneNumber);

        Task CreateContact(Contact contact);
        Task<bool> UpdateContact(Contact contact);
        Task<bool> DeleteContact(string uuid);
    }
}
