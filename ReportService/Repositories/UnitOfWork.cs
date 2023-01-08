using ContactMicroService.Data;
using ContactMicroService.Data.Interfaces;
using ContactMicroService.Entities;
using ReportMicroService.Entities;
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


        public void Dispose()
        {
          
        }

        public Task SaveAsync(Report report)
        {
            throw new NotImplementedException();
        }
    }
}
