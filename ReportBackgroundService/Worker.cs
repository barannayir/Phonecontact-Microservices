using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using ReportBackgroundService.Services;
using System;
using System.Threading.Tasks;
using System.Threading;
using RabbitMQ.Client.Events;
using System.Data;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Text;
using ClosedXML.Excel;
using ReportBackgroundService.Models;
using FastMember;
using EventBusRabbitMQ.Core;

namespace ReportBackgroundService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private RabbitMQClientService _rabbitMQClientService;
        private readonly IServiceProvider _serviceProvider;
        private IModel _channel;

        public Worker(ILogger<Worker> logger, RabbitMQClientService rabbitMQClientService, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _rabbitMQClientService = rabbitMQClientService;
            _serviceProvider = serviceProvider;
        }
        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _channel = _rabbitMQClientService.Connect();
            _channel.BasicQos(0, 1, false);

            return base.StartAsync(cancellationToken);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new AsyncEventingBasicConsumer(_channel);
            _channel.BasicConsume(EventBusConstants.ReportCreateQueue, false, consumer);

            consumer.Received += Consumer_Received;

            return Task.CompletedTask;
        }
        private async Task Consumer_Received(object sender, BasicDeliverEventArgs @event)
        {
            try
            {
                var publishModel = JsonSerializer.Deserialize<Publish>(Encoding.UTF8.GetString(@event.Body.ToArray()));

                using var ms = new MemoryStream();

                DataTable dt = new DataTable() { TableName = "contacts" };
                using (var reader = ObjectReader.Create(publishModel.Contacts))
                {
                    dt.Load(reader);
                }

                var wb = new XLWorkbook();
                wb.Worksheets.Add(dt);

                wb.SaveAs(ms);

                MultipartFormDataContent multipartFormDataContent = new MultipartFormDataContent();
                multipartFormDataContent.Add(new ByteArrayContent(ms.ToArray()), "file", string.Concat(publishModel.Report.FileName, ".xlsx"));

                var baseUrl = $"http://localhost:5274/api/Contacts/UploadExcel?reportFileId={publishModel.Report.uuid}&reportFileName={publishModel.Report.FileName}";
                using var httpClient = new HttpClient();
                var response = await httpClient.PostAsync(baseUrl, multipartFormDataContent);

                if (response.IsSuccessStatusCode)
                {
                    _channel.BasicAck(@event.DeliveryTag, false);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

    }
}
