using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using ride_wise_api.Application.Services.Interfaces;
using System.Text;

namespace ride_wise_api.Application.Services
{
    public class RabbitMqService : IRabbitMqService
    {
        private readonly ConnectionFactory _connectionFactory;
        private readonly IConfiguration _configuration;

        public RabbitMqService(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionFactory = new ConnectionFactory
            {
                HostName = _configuration["RabbitMqConfiguration:Host"],
                UserName = _configuration["RabbitMqConfiguration:Username"],
                Password = _configuration["RabbitMqConfiguration:Password"]
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

        public async Task<byte[]> Consume(string queue, string exchange)
        {
            using var connection = _connectionFactory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.QueueBind(queue: queue, exchange: exchange, routingKey: string.Empty);
            var consumer = new EventingBasicConsumer(channel);
            byte[] message = null;
            consumer.Received += (model, ea) => message = ea.Body.ToArray();
            channel.BasicConsume(queue: queue, autoAck: true, consumer);
            return message;
        }
    }
}
