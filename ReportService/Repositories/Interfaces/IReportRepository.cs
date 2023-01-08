using ReportService.Entities.Dtos;
using Shared.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReportService.Repositories.Interfaces
{
    public interface IReportRepository
    {
        Task<Response<List<ReportDto>>> GetAllAsync();

        Task<Response<ReportDto>> GetByIdAsync(string id);

        Task<Response<ReportDto>> CreateAsync();

        Task<Response<NoContent>> UpdateAsync(ReportDto reportDto);

        Task<Response<NoContent>> DeleteAsync(string id);
    }
}