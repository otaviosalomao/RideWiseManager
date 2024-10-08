using Microsoft.EntityFrameworkCore;

namespace RideWise.Api.Infrastructure
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
