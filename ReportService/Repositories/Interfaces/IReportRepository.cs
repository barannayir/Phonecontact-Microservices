using ReportBackgroundService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReportService.Repositories.Interfaces
{
    public interface IReportRepository
    {
        Task GetAsync(int id);
        Task GetAllAsync();
        Task ExistsAsync(int id);
        Task AddAsync(Report report);
        Task UpdateAsync(Report report);
        Task DeleteAsync(Report report);
        Task Update(Report report);
    }
}
