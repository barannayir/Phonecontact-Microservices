using ContactMicroService.Data;
using ContactMicroService.Data.Interfaces;
using ContactMicroService.Entities;
using ReportService.Repositories.Interfaces;
using System.Threading.Tasks;
using System;
using ReportMicroService.Entities;

namespace ReportService.Repositories
{
    public class ReportFileRepository: ReportRepository<Report>, IReportFileRepository
    {
        private readonly IReportContext _dbContext;
        public ReportFileRepository(IReportContext dbContext) 
        {
            _dbContext = dbContext;
        }
    }
}
