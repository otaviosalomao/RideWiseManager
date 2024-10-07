using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NLog;
using RideWise.Api.Application.Mappings;
using RideWise.Api.Application.Repositories;
using RideWise.Api.Application.Repositories.Interfaces;
using RideWise.Api.Application.Services;
using RideWise.Api.Application.Services.Interfaces;
using RideWise.Api.Infrastructure;
using FluentValidation;
using RideWise.Api.Application.Validators;
using FluentValidation.AspNetCore;
using RideWise.Api.Domain.Services.Interfaces;
using RideWise.Api.Domain.Services;

namespace RideWise.Api.Extensions
{
    public static class ServiceExtensions
    {        
        public static void ConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<RideWiseApiDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("RideWiseApiDatabase")));
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }
        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddTransient<IRentalService, RentalService>();
            services.AddTransient<IDeliveryAgentService, DeliveryAgentService>();
            services.AddTransient<IMotorcycleService, MotorcycleService>();
            services.AddTransient<IRentService, RentService>();
            services.AddTransient<IDriverLicenseFileManager, DriverLicenseFileManager>();
            services.AddScoped<IMessageBusService, MessageBusService>();
            services.AddScoped<IMotorcycleMessageBusProducer, MotorcycleMessageBusProducer>();
            services.AddScoped<IRabbitMqService, RabbitMqService>();
        }
        public static void ConfigureRepositories(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryManager, RepositoryManager>();
            services.AddTransient<IMotorcycleRepository, MotorcycleRepository>();
            services.AddTransient<IDeliveryAgentRepository, DeliveryAgentRepository>();
        }

        public static void ConfigureLogger(this IServiceCollection services)
        {
            LogManager.Setup(option =>
                option.LoadConfigurationFromFile(string.Concat(Directory.GetCurrentDirectory(), "/Nlog.config")));
            services.AddSingleton<ILoggerManager, LoggerManager>();
        }

        public static void ConfigureMapper(this IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MotorcycleRequestMapper());
                mc.AddProfile(new MotorcycleResultMapper());
                mc.AddProfile(new DeliveryAgentRequestMapper());
                mc.AddProfile(new DeliveryAgentResultMapper());
                mc.AddProfile(new RentalRequestMapper());
                mc.AddProfile(new RentalResultMapper());
            });
            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
        }
        public static void AddValidators(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssemblyContaining<MotorcycleRequestValidator>();
            services.AddValidatorsFromAssemblyContaining<DeliveryAgentRequestValidator>();
            services.AddValidatorsFromAssemblyContaining<RentalRequestValidator>();
        }
    }
}
