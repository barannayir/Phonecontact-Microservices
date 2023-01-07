using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Threading.Tasks;
using System;
using ContactMicroService.Entities;

namespace ReportService.Data.Interfaces
{
    public interface IRepository
    {
        Task<IEnumerable<Report>> GetReports();
        Task<Report> GetReport(string uuid);
        Task CreateReport(Report report);
        Task<bool> UpdateReport(Report report);
        Task<bool> DeleteReport(string uuid);
    }
}
