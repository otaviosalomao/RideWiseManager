using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RideWise.Common.Extensions;
using RideWise.Notification.Extensions;
using RideWise.RabbitMqConsumer.Services;
using RideWise.RabbitMqConsumer.Services.Interfaces;

var builder = new ConfigurationBuilder()
    .AddJsonFile("appSettings.json", false, false)
    .AddJsonFile("appSettings.Development.json", false, false)
    .AddEnvironmentVariables();

var config = builder.Build();
IHost _host = Host.CreateDefaultBuilder().ConfigureServices(
    services =>
    {
        services.AddTransient<IMotorcycleNoticeMessageBusConsumerService, MotorcycleNoticeMessaBusConsumerService>();
        services.ConfigureCommonServices();
        services.ConfigureLogger();
        services.ConfigureRabbitMQ(config);
        services.ConfigureDbContext(config);

    }).Build();

var _consumer = _host.Services.GetRequiredService<IMotorcycleNoticeMessageBusConsumerService>();

await _consumer.ProcessAsync();
