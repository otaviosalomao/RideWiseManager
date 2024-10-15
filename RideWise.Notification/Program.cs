using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RideWise.Common.Extensions;
using RideWise.Common.Models;
using RideWise.Common.Services.Interfaces;
using RideWise.Notification.Application.Repositories.Interfaces;
using RideWise.Notification.Domain.Models;
using RideWise.Notification.Extensions;
using RideWise.Notification.Infrastructure;
using System.Text;

var builder = new ConfigurationBuilder()
    .AddJsonFile("appSettings.json", false, false);
var config = builder.Build();
var services = new ServiceCollection();

services.ConfigureDbContext(config);
services.ConfigureRepositories();
services.ConfigureLogger();

var serviceProvider = services.BuildServiceProvider();
MigrationService.InitializaMigration(serviceProvider);

var _repositoryManager = serviceProvider.GetService<IRepositoryManager>();
var _logger = serviceProvider.GetRequiredService<ILoggerManager>();
var motorcycleNotificationQueue = config["Queues:MotorcycleNotification"];
var yearNotificationCriteria = Convert.ToInt32(config["NotificationCriteria:Year"]);
_logger.LogInfo("Start listening");
Console.WriteLine($"Start listening queue: {motorcycleNotificationQueue}");

var factory = new ConnectionFactory
{
    HostName = config["RabbitMqConfiguration:Hostname"],
    UserName = config["RabbitMqConfiguration:Username"],
    Password = config["RabbitMqConfiguration:Password"]
};

var connection = factory.CreateConnection();

using var channel = connection.CreateModel();
channel.QueueDeclare(motorcycleNotificationQueue,
    durable: true,
    exclusive: false,
    autoDelete: false,
    arguments: null);

var consumer = new EventingBasicConsumer(channel);

consumer.Received += (model, eventArgs) =>
{
    var body = eventArgs.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    try
    {
        var result = JsonConvert.DeserializeObject<Motorcycle>(message);
        if (result?.Year == yearNotificationCriteria)
        {
            var motorcycleNotice = new MotorcycleNotice()
            {
                Year = result.Year,
                LicensePlate = result.LicensePlate,
                Model = result.Model
            };
            _repositoryManager.MotorcycleNotice.Create(motorcycleNotice);
            _repositoryManager.Save();
            channel.BasicAck(eventArgs.DeliveryTag, false);
            _logger.LogInfo($"Successfull processing message: {message}");
        }
    }
    catch (Exception ex)
    {
        _logger.LogError($"Failed processing message: {message}");
        channel.BasicNack(eventArgs.DeliveryTag, false, false);
    }
};
channel.BasicConsume(queue: motorcycleNotificationQueue, autoAck: false, consumer: consumer);

Console.ReadKey();