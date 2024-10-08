using RideWise.Api.Application.Services.Interfaces;
using RideWise.RabbitMqConsumer.Services.Interfaces;

namespace RideWise.RabbitMqConsumer.Services
{
    public class MotorcycleNoticeMessaBusConsumerService : IMotorcycleNoticeMessageBusConsumerService
    {
        private readonly IMessageBusService _messageBusService;
        const string QUEUE_NAME = "ride-wise-api.create-motorcycle.queue";
        const string EXCHANGE = "Motorcycle";

        public MotorcycleNoticeMessaBusConsumerService(IMessageBusService messageBusService)
        {
            _messageBusService = messageBusService;
        }

        public async Task ProcessAsync()
        {
            await _messageBusService.Consume(QUEUE_NAME, EXCHANGE);
        }
    }
}
