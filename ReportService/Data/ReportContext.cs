using MongoDB.Driver;
using ReportService.Data.Interfaces;
using ReportService.Entities;
using ReportService.Settings;

namespace ContactMicroService.Data
{
    public class ReportContext : IReportContext
    {
        public ReportContext(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionStrings);
            var database = client.GetDatabase(settings.DatabaseName);

            Reports = database.GetCollection<Report>(settings.ReportCollectionName);
        }

        public IMongoCollection<Report> Reports { get; }
    }
}