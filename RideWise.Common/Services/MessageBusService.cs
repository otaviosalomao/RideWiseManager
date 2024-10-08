using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RideWise.Api.Application.Services.Interfaces;
using System.Text;

namespace RideWise.Api.Application.Services
{
    public class MessageBusService : IMessageBusService
    {
        private readonly ILoggerManager _logger;
        private readonly IRabbitMqService _rabbitMqService;

        public MessageBusService(ILoggerManager logger, IRabbitMqService rabbitMqService)
        {
            _logger = logger;
            _rabbitMqService = rabbitMqService;
        }

        public async Task Publish(string message, string queue, string exchange)
        {
            _logger.LogInfo($"sending to queue {queue} message {message}");
            var body = Encoding.UTF8.GetBytes(message);
            await _rabbitMqService.Publish(body, queue, exchange);
        }
        public async Task<IModel> Consume(string queue, string exchange)
        {
            _logger.LogInfo($"consuming queue {queue}");
            return await _rabbitMqService.Consume(queue, exchange);
        }
    }
}
