using ContactMicroService.Data;
using ContactMicroService.Data.Interfaces;
using ContactMicroService.Entities;
using ReportService.Repositories.Interfaces;
using System.Threading.Tasks;
using System;

namespace ReportService.Repositories
{
    public class ReportFileRepository: ReportRepository<Report>, IReportFileRepository
    {
        private readonly ReportContext _dbContext;

        public ReportFileRepository(ReportContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
