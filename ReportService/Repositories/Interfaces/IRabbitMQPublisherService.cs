using ReportService.Entities;

namespace ReportService.Repositories.Interfaces
{
    public interface IRabbitMQPublisherService
    {
        void Publish(CreateReportEvent createReportEvent, string queue, string routing, string exchange);
    }
}