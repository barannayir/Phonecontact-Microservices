using ContactMicroService.Data;
using ContactMicroService.Data.Interfaces;
using ContactMicroService.Entities;
using ContactMicroService.Repositories.Interfaces;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using System.Runtime.Intrinsics.X86;

namespace ContactMicroService.Repositories
{
    public class ContactRepository : IContactRepository
    {
        public readonly IContactContext _context;
        public ContactRepository(IContactContext context)
        {
            _context = context;
        }
        public async Task CreateContact(Contact contact)
        {
            await _context.Contacts.InsertOneAsync(contact);
        }

        public async Task<bool> DeleteContact(string uuid)
        {
            var filter = Builders<Contact>.Filter.Eq(m => m.uuid, uuid);
            DeleteResult deleteResult = await _context.Contacts.DeleteOneAsync(filter);
            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;    
        }

        public async Task<Contact> GetContact(string uuid)
        {
            return await _context.Contacts.Find(c => c.uuid == uuid).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Contact>> GetContacts()
        {
            return await _context.Contacts.Find(c => true).ToListAsync();
        }

        public async Task<IEnumerable<Contact>> GetContactsByAd(string ad)
        {
            var filter = Builders<Contact>.Filter.Eq(a => a.Ad, ad);
            return await _context.Contacts.Find(filter).ToListAsync();
        }

        public async Task<IEnumerable<Contact>> GetContactsByFirma(string firma)
        {
            var filter = Builders<Contact>.Filter.Eq(a => a.Firma, firma);
            return await _context.Contacts.Find(filter).ToListAsync();
        }

        public async Task<IEnumerable<Contact>> GetContactsByLocation(string location)
        {
            var filter = Builders<Contact>.Filter.ElemMatch(a => a.Location, location);
            return await _context.Contacts.Find(filter).ToListAsync();

        }

        public async Task<Contact> GetContactsByPhoneNumber(string phoneNumber)
        {
            return await _context.Contacts.Find(c => c.PhoneNumber == phoneNumber).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Contact>> GetContactsBySoyad(string soyad)
        {
            var filter = Builders<Contact>.Filter.Eq(s => s.Soyad, soyad);
            return await _context.Contacts.Find(filter).ToListAsync();
        }

        public async Task<bool> UpdateContact(Contact contact)
        {
            var updateResult = await _context.Contacts.ReplaceOneAsync(filter: x => x.uuid == contact.uuid, replacement: contact);
            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }
    }
}
