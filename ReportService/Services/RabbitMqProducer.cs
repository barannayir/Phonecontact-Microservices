using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using ReportService.Services.Interfaces;
using System.Text;
using System;
using Newtonsoft.Json;

namespace ReportService.Services
{
    public class RabbitMqProducer : IMessageProducer
    {
        private readonly IConfiguration _configuration;
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public RabbitMqProducer(IConfiguration configuration)
        {
            _configuration = configuration;
            var factory = new ConnectionFactory()
            {
                HostName = _configuration["RabbitMQ"],
                UserName = _configuration["guess"],
                Password = _configuration["guess"],
                VirtualHost = "/",
            };
            try
            {
                _connection = factory.CreateConnection();
                _channel = _connection.CreateModel();
                _channel.ExchangeDeclare(exchange: "trigger", type: ExchangeType.Fanout);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void SendMessage<T>(T message)
        {
            var json = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(json);
            _channel.BasicPublish(exchange: "trigger", routingKey: "reports", basicProperties: null, body: body);
        }
    }
}
