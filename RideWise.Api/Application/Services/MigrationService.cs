using Microsoft.EntityFrameworkCore;
using RideWise.Api.Infrastructure;

namespace RideWise.Api.Application.Services
{
    public static class MigrationService
    {
        public static void InitializaMigration(this IApplicationBuilder appBuilder)
        {
            using var serviceScope = appBuilder.ApplicationServices.CreateScope();
            serviceScope.ServiceProvider.GetService<RideWiseApiDbContext>()!.Database.Migrate();
        }
    }
}
