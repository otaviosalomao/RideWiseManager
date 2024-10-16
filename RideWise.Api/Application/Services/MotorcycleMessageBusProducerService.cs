﻿using RideWise.Api.Application.Services.Interfaces;
using RideWise.Api.Domain.Models;
using RideWise.Common.Services.Interfaces;
using System.Text.Json;

namespace RideWise.Api.Application.Services
{
    public class MotorcycleMessageBusProducerService : IMotorcycleMessageBusProducerService
    {
        readonly IMessageBusService _messageBusService;
        readonly ILoggerManager _logger;
        const string QUEUE_NAME = "ride-wise-api.create-motorcycle.queue";
        const string EXCHANGE = "Motorcycle";

        public MotorcycleMessageBusProducerService(IMessageBusService messageBusService, ILoggerManager logger)
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
