using ContactMicroService.Data;
using ContactMicroService.Data.Interfaces;
using ContactMicroService.Entities;
using ReportService.Repositories.Interfaces;
using System;
using System.Threading.Tasks;

namespace ReportService.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ReportContext _dbContext;
        private IReportFileRepository _reportFileRepository;
        public UnitOfWork(ReportContext dbContext) 
        {
            _dbContext = dbContext;
        }

        public IReportFileRepository ReportFileRepository => _reportFileRepository ??= new ReportFileRepository(_dbContext);


        public async Task SaveAsync(Report report)
        {
            
        }

        public void Dispose()
        {
      
            GC.SuppressFinalize(this);
        }

    }
}
