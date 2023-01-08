using ContactMicroService.Entities.Dtos;
using Shared.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContactMicroService.Repositories.Interfaces
{
    public interface ICommunicationRepository
    {
        Task<Response<List<CommunicationDto>>> GetAllAsync();

        Task<Response<List<CommunicationDto>>> GetAllByContactIdAsync(string contactId);

        Task<Response<List<CommunicationDto>>> GetAllByContactIdsAsync(List<string> contactIds);

        Task<Response<CommunicationDto>> GetByIdAsync(string id);

        Task<Response<CommunicationDto>> CreateAsync(CommunicationCreateDto communication);

        Task<Response<NoContent>> UpdateAsync(CommunicationUpdateDto communication);

        Task<Response<NoContent>> DeleteAsync(string id);
    }
}