using ContactMicroService.Data;
using ContactMicroService.Data.Interfaces;
using ContactMicroService.Entities;
using MongoDB.Driver;
using ReportService.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ReportService.Data
{
    public class Repository : IRepository
    {
        public readonly IReportContext _context;
        public Repository(IReportContext context)
        {
            _context = context;
        }

        public Task CreateReport(Report report)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteReport(string uuid)
        {
            throw new NotImplementedException();
        }

        public Task<Report> GetReport(string uuid)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Report>> GetReports()
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateReport(Report report)
        {
            throw new NotImplementedException();
        }
    }
}
