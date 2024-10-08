using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RideWise.Api.Application.Services.Interfaces;
using RideWise.Common.Models;
using System.Text;
using System.Threading.Channels;
using static MassTransit.Monitoring.Performance.BuiltInCounters;

namespace RideWise.Api.Application.Services
{
    public class RabbitMqService : DefaultBasicConsumer, IRabbitMqService
    {
        private readonly ConnectionFactory _connectionFactory;
        public RabbitMqService(RabbitMqConfiguration rabbitMqConfiguration)
        {
            _connectionFactory = new ConnectionFactory
            {
                HostName = rabbitMqConfiguration.HostName,
                UserName = rabbitMqConfiguration.UserName,
                Password = rabbitMqConfiguration.Password
            };
        }

        public async Task Publish(byte[] body, string queue, string exchange)
        {
            using var connection = _connectionFactory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.QueueBind(queue: queue, exchange: exchange, routingKey: string.Empty);
            channel.BasicPublish(exchange: exchange,
                                 routingKey: string.Empty,
                                 basicProperties: null,
                                 body: body);
        }            
    }
}
