using AutoMapper;
using ContactMicroService.Entities;
using ContactMicroService.Entities.Dtos;
using ContactMicroService.Repositories.Interfaces;
using ContactMicroService.Settings;
using MongoDB.Driver;
using Shared.Dtos;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactMicroService.Repositories
{
    public class CommunicationRepository : ICommunicationRepository
    {
        private readonly IMongoCollection<Communication> _communicationCollection;
        private readonly IMapper _mapper;

        public CommunicationRepository(IMapper mapper, IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionStrings);
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            _communicationCollection = database.GetCollection<Communication>(databaseSettings.CommunicationCollectionName);

            _mapper = mapper;
        }

        public async Task<Response<List<CommunicationDto>>> GetAllAsync()
        {
            var communications = await _communicationCollection.Find(x => true).ToListAsync();
            return Response<List<CommunicationDto>>.Success(_mapper.Map<List<CommunicationDto>>(communications), 200);
        }

        public async Task<Response<List<CommunicationDto>>> GetAllByContactIdAsync(string contactId)
        {
            var communications = await _communicationCollection.Find(x => x.ContactId == contactId).ToListAsync();
            if (communications.Any())
            {
                return Response<List<CommunicationDto>>.Success(_mapper.Map<List<CommunicationDto>>(communications), 200);
            }
            return Response<List<CommunicationDto>>.Fail("Communications not found!", 404);
        }

        public async Task<Response<List<CommunicationDto>>> GetAllByContactIdsAsync(List<string> contactIds)
        {
            var filter = Builders<Communication>.Filter.In(x => x.ContactId, contactIds);
            var communications = await _communicationCollection.Find(filter).ToListAsync();
            if (communications.Any())
            {
                return Response<List<CommunicationDto>>.Success(_mapper.Map<List<CommunicationDto>>(communications), 200);
            }
            return Response<List<CommunicationDto>>.Fail("Communications not found!", 404);
        }

        public async Task<Response<CommunicationDto>> GetByIdAsync(string id)
        {
            var communication = await _communicationCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
            if (communication == null)
            {
                return Response<CommunicationDto>.Fail("Communication not found!", 404);
            }
            return Response<CommunicationDto>.Success(_mapper.Map<CommunicationDto>(communication), 200);
        }

        public async Task<Response<CommunicationDto>> CreateAsync(CommunicationCreateDto communication)
        {
            var newCommunication = _mapper.Map<Communication>(communication);
            await _communicationCollection.InsertOneAsync(newCommunication);
            return Response<CommunicationDto>.Success(_mapper.Map<CommunicationDto>(newCommunication), 200);
        }

        public async Task<Response<NoContent>> UpdateAsync(CommunicationUpdateDto communication)
        {
            var updateCoummunication = _mapper.Map<Communication>(communication);
            var result = await _communicationCollection.FindOneAndReplaceAsync(x => x.Id == updateCoummunication.Id, updateCoummunication);
            if (result == null)
            {
                return Response<NoContent>.Fail("Communications not found!", 404);
            }
            return Response<NoContent>.Success(204);
        }

        public async Task<Response<NoContent>> DeleteAsync(string id)
        {
            var result = await _communicationCollection.DeleteOneAsync(x => x.Id == id);
            if (result.DeletedCount > 0)
            {
                return Response<NoContent>.Fail("Communications not found!", 404);
            }
            return Response<NoContent>.Success(204);
        }
    }
}