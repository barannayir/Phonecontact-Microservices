using AutoMapper;
using ContactMicroService.Entities.Dtos;
using ContactMicroService.Repositories.Interfaces;
using ContactMicroService.Settings;
using MongoDB.Driver;
using Shared.Dtos;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContactModel = ContactMicroService.Entities.Contact;

namespace ContactMicroService.Repositories
{
    public class ContactRepository : IContactRepository
    {
        private readonly IMongoCollection<ContactModel> _contactCollection;
        private readonly ICommunicationRepository _communicationService;
        private readonly IMapper _mapper;

        public ContactRepository(IMapper mapper, IDatabaseSettings databaseSettings, ICommunicationRepository communicationService)
        {
            var client = new MongoClient(databaseSettings.ConnectionStrings);
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            _contactCollection = database.GetCollection<ContactModel>(databaseSettings.ContactCollectionName);
            _mapper = mapper;
            _communicationService = communicationService;
        }

        public async Task<Response<List<ContactDto>>> GetAllAsync()
        {
            var contacts = await _contactCollection.Find(x => true).ToListAsync();
            return Response<List<ContactDto>>.Success(_mapper.Map<List<ContactDto>>(contacts), 200);
        }

        public async Task<Response<List<ContactStatisticsDto>>> GetAllContactWithCommunicationsAsync()
        {
            List<ContactStatisticsDto> contactStatistics = new List<ContactStatisticsDto>();
            var contacts = await _contactCollection.Find(x => true).ToListAsync();
            var contactIds = contacts.Select(x => x.Id).ToList();
            var communications = (await _communicationService.GetAllByContactIdsAsync(contactIds)).Data;

            var locationGroup = communications.Where(x => x.CommunicationType == CommunicationType.LOCATION)
                .GroupBy(x => x.Address).Select(x => x.Key).ToList();

            locationGroup.ForEach(location =>
            {
                var contactIdsOfLocation = communications.Where(x => x.Address == location && x.CommunicationType == CommunicationType.LOCATION)
                .Select(x => x.ContactId).Distinct().ToList();

                var phoneCountOfLocation = communications.Where(x => contactIdsOfLocation.Contains(x.ContactId) && x.CommunicationType == CommunicationType.PHONE)
                .Select(x => x.Address).Distinct().Count();

                contactStatistics.Add(new ContactStatisticsDto
                {
                    Location = location,
                    ContactCount = contactIdsOfLocation.Count,
                    PhoneCount = phoneCountOfLocation
                });
            });

            return Response<List<ContactStatisticsDto>>.Success(contactStatistics, 200);
        }

        public async Task<Response<ContactWithCommunicationsDto>> GetById(string id)
        {
            var contact = await _contactCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
            if (contact == null)
            {
                return Response<ContactWithCommunicationsDto>.Fail("Contact not found!", 404);
            }
            var contactDto = _mapper.Map<ContactWithCommunicationsDto>(contact);
            var communicationsResponse = await _communicationService.GetAllByContactIdAsync(contact.Id);
            contactDto.Communications = communicationsResponse.Data;
            return Response<ContactWithCommunicationsDto>.Success(contactDto, 200);
        }

        public async Task<Response<ContactDto>> CreateAsync(ContactCreateDto contact)
        {
            var newContact = _mapper.Map<ContactModel>(contact);
            await _contactCollection.InsertOneAsync(newContact);
            return Response<ContactDto>.Success(_mapper.Map<ContactDto>(newContact), 200);
        }

        public async Task<Response<NoContent>> UpdateAsync(ContactUpdateDto contact)
        {
            var updateContact = _mapper.Map<ContactModel>(contact);
            var result = await _contactCollection.FindOneAndReplaceAsync(x => x.Id == updateContact.Id, updateContact);
            if (result == null)
            {
                return Response<NoContent>.Fail("Contact not found!", 404);
            }
            return Response<NoContent>.Success(204);
        }

        public async Task<Response<NoContent>> DeleteAsync(string id)
        {
            var result = await _contactCollection.DeleteOneAsync(x => x.Id == id);
            if (result.DeletedCount > 0)
            {
                return Response<NoContent>.Fail("Contact not found!", 404);
            }
            return Response<NoContent>.Success(204);
        }
    }
}