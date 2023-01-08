using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Collections.Generic;
using System.IO;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using OfficeOpenXml;
using ReportService.Entities.Dtos;
using ReportService.Http.Interface;
using ReportService.Repositories.Interfaces;
using ContactMicroService.Entities;
using ContactMicroService.Data.Interfaces;
using MongoDB.Driver;
using ReportMicroService.Entities;
using ReportService.Entities;

namespace ReportService.Services
{
    public class ReportBackgroundService : BackgroundService
    {
        private IConfiguration _configuration;
        private IConnection _connection;
        private IModel _channel;
        private string _queueName;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IContactContext _contactContext;

        public ReportBackgroundService(IConfiguration configuration, IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
            _configuration = configuration;
            Init();

        }
        private void Init()
        {

            var factory = new ConnectionFactory()
            {
                HostName = _configuration["RabbitMQ"],
                UserName = _configuration["guess"],
                Password = _configuration["guess"],
                VirtualHost = "/",
            };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.ExchangeDeclare(type: ExchangeType.Fanout, exchange: "trigger");
            _queueName = _channel.QueueDeclare().QueueName;
            _channel.QueueBind(queue: _queueName, exchange: "trigger", routingKey: "");
        }
        
        
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (ModuleHandle, ea) =>
            {
                var body = ea.Body;
                var notification = Encoding.UTF8.GetString(body.ToArray());
                var json = JsonConvert.DeserializeObject<GenerateReport>(notification);
                if (json.Status == "ReportCreating")
                {
                    CreateReport(notification);
                }
            };
            _channel.BasicConsume(queue: _queueName, autoAck: true, consumer: consumer);
            return Task.CompletedTask;
        }

        public async Task CreateReport(string reportMessage)
        {
            List<ReportResultDto> resultList = new List<ReportResultDto>();
            using (var scope = _scopeFactory.CreateScope())
            {
                var repo = scope.ServiceProvider.GetRequiredService<IReportRepository>();
                var contactClient = scope.ServiceProvider.GetRequiredService<IContactClient>();
                var reportRequestDto = JsonConvert.DeserializeObject<GenerateReport>(reportMessage);
                try
                {
                    var reportObj = await repo.GetAsync(reportRequestDto.ReportId);

                    var allInfos = contactClient.GetAllInformations().Result;
                    var filter = Builders<Contact>.Filter.Eq(a => a.Location, ReportEntities.Location);
                    var locations = _contactContext.Contacts.Find(filter).ToListAsync();

                    var path = Path.Combine(Directory.GetCurrentDirectory(), "Reports");
                    string fileName = $@"{reportRequestDto.ReportId}.xlsx";
                    FileInfo file = new FileInfo(Path.Combine(path, fileName));
                    if (file.Exists)
                    {
                        file.Delete();
                        file = new FileInfo(Path.Combine(path, fileName));
                    }
                    using (ExcelPackage pckg = new ExcelPackage(file))
                    {

                        ExcelWorksheet _excel = pckg.Workbook.Worksheets.Add("Report");
                        _excel.Cells[1, 1].Value = "Konum Bilgisi";
                        _excel.Cells[1, 2].Value = "Kişi Sayısı";
                        _excel.Cells[1, 3].Value = "Telefon Numarası";


                        for (int i = 0; i < resultList.Count; i++)
                        {
                            _excel.Cells[$"A{i + 2}"].Value = resultList[i].Location;
                            _excel.Cells[$"B{i + 2}"].Value = resultList[i].ContactCount;
                            _excel.Cells[$"C{i + 2}"].Value = resultList[i].ContactNumber;

                        }
                        pckg.Save();
                    }
                    reportObj.Status = ReportStatus.Completed;
                    await repo.Update(reportObj).ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                }
            }
        }

        
    }
}
