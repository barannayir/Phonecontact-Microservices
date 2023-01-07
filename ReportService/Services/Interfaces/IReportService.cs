using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using ReportService.Entities.Dtos;

namespace ReportService.Services.Interfaces
{
    public interface IReportService
    {
        List<GetReport> GetReports();
        Task<string> Add(ReportRequest report);
    }
}
