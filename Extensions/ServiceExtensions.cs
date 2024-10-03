using ride_wise_api.Application.Repositories.Interfaces;
using ride_wise_api.Application.Repositories;
using ride_wise_api.Application.Services.Interfaces;
using ride_wise_api.Application.Services;
using ride_wise_api.Infrastructure;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using ride_wise_api.Application.Mappings;
using NLog;

namespace ride_wise_api.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<RiseWiseManagerDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("RiseWiseManagerDatabase")));
        }
        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddTransient<IRentalService, RentalService>();
            services.AddTransient<IDeliveryAgentService, DeliveryAgentService>();
            services.AddTransient<IMotorCycleService, MotorCycleService>();
        }
        public static void ConfigureRepositories(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryManager, RepositoryManager>();
            services.AddTransient<IMotorCycleRepository, MotorCycleRepository>();
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
                mc.AddProfile(new MappingProfile());
            });
            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            
        }
    }
}
