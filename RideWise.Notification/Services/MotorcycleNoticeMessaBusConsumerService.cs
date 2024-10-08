using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RideWise.Api.Application.Services.Interfaces;
using RideWise.Api.Domain.Models;
using RideWise.Notification.Application.Repositories.Interfaces;
using RideWise.Notification.Domain.Models;
using RideWise.RabbitMqConsumer.Services.Interfaces;
using System.Text;
using System.Threading.Channels;

namespace RideWise.RabbitMqConsumer.Services
{
    public class MotorcycleNoticeMessaBusConsumerService : IMotorcycleNoticeMessageBusConsumerService
    {
        private readonly IMessageBusService _messageBusService;
        const string QUEUE_NAME = "ride-wise-api.create-motorcycle.queue";
        const string EXCHANGE = "Motorcycle";
        private readonly IRepositoryManager _repositoryManager;

        public MotorcycleNoticeMessaBusConsumerService(
            IRepositoryManager repositoryManager,
            IMessageBusService messageBusService)
        {
            _repositoryManager = repositoryManager;
            _messageBusService = messageBusService;
        }

        public async Task ProcessAsync()
        {
            var channel = await _messageBusService.Consume(QUEUE_NAME, EXCHANGE);
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += ( model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var result = JsonConvert.DeserializeObject<Motorcycle>(message);
                if(result.Year == 2024)
                {
                    var motorcycleNotice = new MotorcycleNotice()
                    {
                        Year = result.Year,
                        LicensePlate = result.LicensePlate,
                        Model = result.Model
                    };
                    _repositoryManager.MotorcycleNotice.Create(motorcycleNotice);
                    _repositoryManager.Save();
                }                
            };
            channel.BasicConsume(queue: QUEUE_NAME, autoAck: true, consumer);
            channel.Close();
        }
    }
}
