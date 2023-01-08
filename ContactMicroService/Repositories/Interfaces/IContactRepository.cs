using ContactMicroService.Entities.Dtos;
using Shared.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContactMicroService.Repositories.Interfaces
{
    public interface IContactRepository
    {
        Task<Response<List<ContactDto>>> GetAllAsync();

        Task<Response<ContactWithCommunicationsDto>> GetById(string id);

        Task<Response<List<ContactStatisticsDto>>> GetAllContactWithCommunicationsAsync();

        Task<Response<ContactDto>> CreateAsync(ContactCreateDto contact);

        Task<Response<NoContent>> UpdateAsync(ContactUpdateDto contact);

        Task<Response<NoContent>> DeleteAsync(string id);
    }
}