using ContactMicroService.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;

namespace ContactMicroService.Data
{
    public class ContactsContextSeed
    {
        public static void SeedData(IMongoCollection<Contact> contactCollection)
        {
            bool existContact = contactCollection.Find(p => true).Any();
            if (!existContact)
            {
                contactCollection.InsertManyAsync(GetPreconfiguredContacts());
            }
        }

        private static IEnumerable<Contact> GetPreconfiguredContacts()
        {
            return new List<Contact>()
            {
                new Contact(){
                    uuid= "1",
                    Ad = "Baran",
                    Soyad = "Nayır",
                    Firma = "A Firması",
                            PhoneNumber = "0532 123 45 67",
                            Email = "barannayir@gmail.com",
                            Location = "İstanbul"
                },
                new Contact(){
                    uuid= "2",
                    Ad = "Ahmet",
                    Soyad = "Nayır",
                    Firma = "A Firması",
                            PhoneNumber = "0532 123 45 67",
                            Email = "ahmet@gmail.com",
                            Location = "Ankara"
                },
                new Contact(){
                    uuid= "3",
                    Ad = "Mehmet",
                    Soyad = "Nayır",
                    Firma = "A Firması",
                            PhoneNumber = "0532 123 45 67",
                            Email = "mehmet@gmail.com",
                            Location = "İstanbul"
                }
            };
        }
    }
}
