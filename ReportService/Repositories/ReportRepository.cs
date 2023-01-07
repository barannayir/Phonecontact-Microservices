using ContactMicroService.Data;
using ContactMicroService.Data.Interfaces;
using ContactMicroService.Entities;
using ReportService.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReportService.Repositories
{
    public class ReportRepository<T> : IReportRepository
    {
        private readonly ReportContext _dbContext;

        public Task AddAsync(ReportBackgroundService.Models.Report report)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteAsync(ReportBackgroundService.Models.Report report)
        {
            throw new System.NotImplementedException();
        }

        public Task ExistsAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task GetAllAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task GetAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task Update(ReportBackgroundService.Models.Report report)
        {
            throw new System.NotImplementedException();
        }

        public Task UpdateAsync(ReportBackgroundService.Models.Report report)
        {
            throw new System.NotImplementedException();
        }
    }
}
