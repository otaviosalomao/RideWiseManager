using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using RideWise.Notification.Extensions;
using System.Text;
using RideWise.Common.Models;
using Newtonsoft.Json;
using RideWise.Notification.Domain.Models;
using RideWise.Notification.Application.Repositories.Interfaces;

var builder = new ConfigurationBuilder()
    .AddJsonFile("appSettings.json", false, false)
    .AddJsonFile("appSettings.Development.json", false, false)
    .AddEnvironmentVariables();

var config = builder.Build();
IHost _host = Host.CreateDefaultBuilder().ConfigureServices(
    services =>
    {
        services.ConfigureRabbitMQ(config);
        services.ConfigureDbContext(config);
        services.ConfigureRepositories();
        services.ConfigureRabbitMQ(config);

    }).Build();

var _repositoryManager = _host.Services.GetRequiredService<IRepositoryManager>();

var factory = new ConnectionFactory
{
    HostName = config["RabbitMqConfiguration:Host"],
    UserName = config["RabbitMqConfiguration:Username"],
    Password = config["RabbitMqConfiguration:Password"]
};

var connection = factory.CreateConnection();

using var channel = connection.CreateModel();
channel.QueueDeclare("ride-wise-api.create-motorcycle.queue",
    durable: true,
    exclusive: false,
    autoDelete: false,
    arguments: null);

var consumer = new EventingBasicConsumer(channel);

consumer.Received += (model, eventArgs) =>
{
    try
    {
        var body = eventArgs.Body.ToArray();
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
            channel.BasicAck(eventArgs.DeliveryTag, false);
        }
    }
    catch (Exception ex)
    {
        channel.BasicNack(eventArgs.DeliveryTag, false, true);
    }
};
channel.BasicConsume(queue: "ride-wise-api.create-motorcycle.queue", autoAck: false, consumer: consumer);

Console.ReadKey();