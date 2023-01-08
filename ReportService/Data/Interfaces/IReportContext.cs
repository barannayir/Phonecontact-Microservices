using MongoDB.Driver;
using ReportService.Entities;

namespace ReportService.Data.Interfaces
{
    public interface IReportContext
    {
        IMongoCollection<Report> Reports { get; }
    }
}