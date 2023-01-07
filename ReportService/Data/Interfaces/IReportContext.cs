using ContactMicroService.Entities;
using MongoDB.Driver;

namespace ContactMicroService.Data.Interfaces
{
    public interface IReportContext
    {
        IMongoCollection<Report> Reports { get;  }
    }
}
