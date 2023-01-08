using RabbitMQ.Client;
using ReportService.Entities;
using ReportService.Repositories.Interfaces;
using System.Text;
using System.Text.Json;

namespace ReportService.Repositories
{
    public class RabbitMQPublisherService : IRabbitMQPublisherService
    {
        private readonly RabbitMQClientService _rabbitMQClientService;

        public RabbitMQPublisherService(RabbitMQClientService rabbitMQClientService)
        {
            _rabbitMQClientService = rabbitMQClientService;
        }

        public void Publish(CreateReportEvent createReportEvent, string queue, string routing, string exchange)
        {
            var channel = _rabbitMQClientService.Connect(queue, routing, exchange);
            var bodyString = JsonSerializer.Serialize(createReportEvent);
            var bodyByte = Encoding.UTF8.GetBytes(bodyString);

            var properties = channel.CreateBasicProperties();
            properties.Persistent = true;

            channel.BasicPublish(exchange, routing, properties, bodyByte);
        }
    }
}