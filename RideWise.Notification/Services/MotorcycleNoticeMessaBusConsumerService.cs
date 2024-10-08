using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RideWise.Api.Application.Services.Interfaces;
using RideWise.Api.Domain.Models;
using RideWise.Common.Models;
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
        private readonly ConnectionFactory _connectionFactory;

        public MotorcycleNoticeMessaBusConsumerService(
            IRepositoryManager repositoryManager,
            IMessageBusService messageBusService,
            RabbitMqConfiguration rabbitMqConfiguration)
        {
            _repositoryManager = repositoryManager;
            _messageBusService = messageBusService;
            _connectionFactory = new ConnectionFactory
            {
                HostName = rabbitMqConfiguration.HostName,
                UserName = rabbitMqConfiguration.UserName,
                Password = rabbitMqConfiguration.Password
            };
        }
        public async Task ProcessAsync()
        {
            using var connection = _connectionFactory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: QUEUE_NAME,
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            Console.WriteLine(" [*] Waiting for messages.");

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var result = JsonConvert.DeserializeObject<Motorcycle>(message);
                if (result?.Year == 2024)
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
            channel.BasicConsume(queue: QUEUE_NAME,
                                 autoAck: true,
                                 consumer: consumer);
        }
    }
}
