﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RideWise.Common.Models;
using RideWise.Notification.Application.Repositories;
using RideWise.Notification.Application.Repositories.Interfaces;
using RideWise.Notification.Infrastructure;

namespace RideWise.Notification.Extensions
{
    internal static class ServiceExtensions
    {
        public static void ConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<RideWiseNotificationDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("RideWiseNotificationDatabase")));
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }

        public static void ConfigureRabbitMQ(this IServiceCollection services, IConfiguration configuration)
        {
            var rabbitMqConfiguration = new RabbitMqConfiguration()
            {
                HostName = configuration["RabbitMqConfiguration:Host"],
                UserName = configuration["RabbitMqConfiguration:Username"],
                Password = configuration["RabbitMqConfiguration:Password"]
            };            
            services.AddSingleton(rabbitMqConfiguration);
        }

        public static void ConfigureRepositories(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryManager, RepositoryManager>();
        }
    }
}
