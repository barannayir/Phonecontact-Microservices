using ContactMicroService.Data;
using MongoDB.Driver;
using ReportMicroService.Entities;
using ReportService.Repositories.Interfaces;
using System;
using System.Threading.Tasks;

namespace ReportService.Repositories
{
    public class ReportRepository<T> : IReportRepository
    {
        private readonly ReportContext _dbContext;

        public ReportRepository(ReportContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task AddAsync(Report report)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteAsync(Report report)
        {
            throw new System.NotImplementedException();
        }

        public Task ExistsAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task ExistsAsync(string id)
        {
            throw new System.NotImplementedException();
        }

        public Task GetAllAsync()
        {
            throw new System.NotImplementedException();
        }




        public Task Update(Report report)
        {
            throw new System.NotImplementedException();
        }

        public Task UpdateAsync(Report report)
        {
            throw new System.NotImplementedException();
        }

        Task<bool> IReportRepository.DeleteAsync(Report report)
        {
            throw new NotImplementedException();
        }

        async Task<Report> IReportRepository.GetAsync(string id)
        {

            return  await _dbContext.Reports.Find(c => c.uuid == id).FirstOrDefaultAsync();
        }

        Task<bool> IReportRepository.Update(Report report)
        {
            throw new NotImplementedException();
        }

        Task<bool> IReportRepository.UpdateAsync(Report report)
        {
            throw new NotImplementedException();
        }
    }
}
