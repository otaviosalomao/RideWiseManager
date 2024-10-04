using ride_wise_api.Application.Services.Interfaces;
using ride_wise_api.Domain.Models;
using System.Text.Json;

namespace ride_wise_api.Application.Services
{
    public class MotorcycleMessageBusProducer : IMotorcycleMessageBusProducer
    {
        readonly IMessageBusService _messageBusService;
        readonly ILoggerManager _logger;
        const string QUEUE_NAME = "ride-wise-api.create-motorcycle.queue";
        const string EXCHANGE = "Motorcycle";

        public MotorcycleMessageBusProducer(IMessageBusService messageBusService, ILoggerManager logger)
        {
            _messageBusService = messageBusService;
            _logger = logger;
        }

        public async Task Publish(Motorcycle motorcycle)
        {
            _logger.LogInfo($"Publish Motorcycle create event for {motorcycle}");
            var modelJson = JsonSerializer.Serialize(motorcycle);

            await _messageBusService.Publish(modelJson, QUEUE_NAME, EXCHANGE);
        }
    }
}
