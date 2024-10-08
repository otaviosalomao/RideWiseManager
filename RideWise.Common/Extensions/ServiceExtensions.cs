using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NLog;
using RideWise.Api.Application.Services;
using RideWise.Api.Application.Services.Interfaces;
using RideWise.Common.Models;

namespace RideWise.Common.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureCommonServices(this IServiceCollection services)
        {
            services.AddScoped<IMessageBusService, MessageBusService>();
        }
        public static void ConfigureRabbitMQ(this IServiceCollection services, IConfiguration configuration)
        {
            var rabbitMqConfiguration = new RabbitMqConfiguration()
            {
                HostName = configuration["RabbitMqConfiguration:Host"],
                UserName = configuration["RabbitMqConfiguration:Username"],
                Password = configuration["RabbitMqConfiguration:Password"]
            };
            services.AddScoped<IRabbitMqService, RabbitMqService>();
            services.AddSingleton(rabbitMqConfiguration);
        }
        public static void ConfigureLogger(this IServiceCollection services)
        {
            LogManager.Setup(option =>
                option.LoadConfigurationFromFile(string.Concat(Directory.GetCurrentDirectory(), "/Nlog.config")));
            services.AddSingleton<ILoggerManager, LoggerManager>();
        }
    }
}
