using System.Collections.Generic;
using System.Threading.Tasks;
using ReportMicroService.Entities;

namespace ReportService.Repositories.Interfaces
{
    public interface IReportRepository
    {
        Task<Report> GetAsync(string id);
        Task GetAllAsync();
        Task ExistsAsync(string id);
        Task AddAsync(Report report);
        Task<bool> UpdateAsync(Report report);
        Task<bool> DeleteAsync(Report report);
        Task<bool> Update(Report report);
    }
}
