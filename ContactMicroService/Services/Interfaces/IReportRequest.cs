using System.Threading.Tasks;
using ContactMicroService.Entities;
namespace ContactMicroService.Services.Interfaces
{
    public interface IReportRequest
    {
        Task<Response> SendReportRequest(Contact contact);
    }
}
