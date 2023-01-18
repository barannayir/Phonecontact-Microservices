using ClosedXML.Excel;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using ReportService.Entities;
using ReportService.Entities.Dtos;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace ReportService.Repositories
{
    public class ExcelReportBackgroundService : BackgroundService
    {
        private readonly RabbitMQClientService _rabbitMQClientService;
        private readonly HttpClientService _httpClientService;
        private IModel _channel;
        private readonly string _contactReportDataGetUrl;
        private readonly string _reportUpdateUrl;
        private ILogger<ExcelReportBackgroundService> _logger;

        public ExcelReportBackgroundService(RabbitMQClientService rabbitMQClientService, HttpClientService httpClientService, IOptions<MicroServices> microServices, ILogger<ExcelReportBackgroundService> logger)
        {
            _logger = logger;
            _rabbitMQClientService = rabbitMQClientService;
            _httpClientService = httpClientService;
            _contactReportDataGetUrl = $"{microServices.Value.ContactService.Domain}{Constant.ContactGetReportData}";
            _reportUpdateUrl = $"{microServices.Value.ReportService.Domain}{Constant.ReportUpdateUrl}";
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            Task.Delay(5000).Wait();
            _channel = _rabbitMQClientService.Connect(Constant.ReportQueue, Constant.ReportRouting, Constant.ReportExchange);
            _channel.BasicQos(0, 1, false);
            return base.StartAsync(cancellationToken);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new AsyncEventingBasicConsumer(_channel);
            _channel.BasicConsume(Constant.ReportQueue, false, consumer);
            consumer.Received += Consumer_Received;
            return Task.CompletedTask;
        }

        private async Task Consumer_Received(object sender, BasicDeliverEventArgs @event)
        {
            var reportEvent = JsonSerializer.Deserialize<CreateReportEvent>(Encoding.UTF8.GetString(@event.Body.ToArray()));
            var path = Path.Combine("./", "wwwRoot", reportEvent.ReportName + ".xlsx");
            try
            {
                var reportDataRaw = await _httpClientService.GetAsync(_contactReportDataGetUrl);
                await UpdateReportInformationsAsync(reportEvent, path, ReportStatusType.INPROGRESS);

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var contactsResponse = JsonSerializer.Deserialize<Response<List<ContactStatisticsDto>>>(reportDataRaw.Data, options);

                CreateExcel(contactsResponse.Data, path);

                Task.Delay(5000).Wait();

                await UpdateReportInformationsAsync(reportEvent, path, ReportStatusType.COMPLETED);

                _channel.BasicAck(@event.DeliveryTag, false);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                await UpdateReportInformationsAsync(reportEvent, path, ReportStatusType.FAILED);
            }
        }

        private async Task UpdateReportInformationsAsync(CreateReportEvent reportEvent, string path, ReportStatusType reportStatusType)
        {
            StringContent stringContent = CreateUpdateRequest(reportEvent, path, reportStatusType);
            await _httpClientService.PostPutAsync(_reportUpdateUrl, stringContent, true);
        }

        private static StringContent CreateUpdateRequest(CreateReportEvent reportEvent, string path, ReportStatusType reportStatusType)
        {
            ReportDto reportDto = new ReportDto { Id = reportEvent.ReportId, CreatedDate = DateTime.UtcNow, FilePath = path, Status = reportStatusType };
            var jsonReportDto = JsonSerializer.Serialize(reportDto);
            StringContent stringContent = new StringContent(jsonReportDto, Encoding.UTF8, "application/json");
            return stringContent;
        }

        private static void CreateExcel(List<ContactStatisticsDto> reportDatas, string path)
        {
            using (IXLWorkbook workbook = new XLWorkbook())
            {
                workbook.AddWorksheet("ReportWithLocations").FirstCell().InsertTable<ContactStatisticsDto>(reportDatas, false);
                workbook.SaveAs(path);
            }
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            return base.StopAsync(cancellationToken);
        }
    }
}