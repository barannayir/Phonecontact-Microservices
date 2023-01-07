using ContactMicroService.Data;
using ContactMicroService.Data.Interfaces;
using ContactMicroService.Entities;
using ReportService.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReportService.Repositories
{
    public class ReportRepository<T> : IReportRepository<T> where T : class
    {
        private readonly ReportContext _dbContext;

        public Task<T> AddAsync(T entity)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteAsync(T entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> ExistsAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<IReadOnlyList<T>> GetAllAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<T> GetAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task UpdateAsync(T entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
