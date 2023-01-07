using ReportService.Entities.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReportService.Http.Interface
{
    public interface IContactClient
    {
        Task<IEnumerable<ContactInfo>> GetAllInformations();
    }
}
