using ContactMicroService.Data.Interfaces;
using ContactMicroService.Entities;
using ContactMicroService.Settings;
using MongoDB.Driver;

namespace ContactMicroService.Data
{
    public class ReportContext : IReportContext
    {
        public ReportContext(IContactDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionStrings);
            var database = client.GetDatabase(settings.DatabaseName);

            Reports = database.GetCollection<Report>(settings.CollectionName);            
        }
        public IMongoCollection<Report> Reports { get;  }

        
    }
}
