using RabbitMQ.Client;
using RideWise.Common.Models;

namespace RideWise.Common.Infrastructure
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
            channel.ExchangeDeclare(exchange: exchange, type: ExchangeType.Fanout);
            channel.QueueDeclare(queue: queue, durable: true, autoDelete: false, exclusive: false);
            channel.QueueBind(queue: queue, exchange: exchange, routingKey: string.Empty);
            channel.BasicPublish(exchange: exchange,
                                 routingKey: string.Empty,
                                 basicProperties: null,
                                 body: body);
        }            
    }
}
