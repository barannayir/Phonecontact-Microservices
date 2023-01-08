using ContactMicroService.Entities;
using MongoDB.Driver;
using ReportMicroService.Entities;

namespace ContactMicroService.Data.Interfaces
{
    public interface IReportContext
    {
        IMongoCollection<Report> Reports { get;  }
    }
}
