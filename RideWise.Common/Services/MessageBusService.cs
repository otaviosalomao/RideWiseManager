﻿using RideWise.Common.Infrastructure;
using RideWise.Common.Services.Interfaces;
using System.Text;

namespace RideWise.Common.Services
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
    }
}
