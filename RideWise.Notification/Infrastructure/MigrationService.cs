using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

public static class MigrationExtensions
{
    public static IHost MigrateDatabase<T>(this IHost host) where T : DbContext
    {
        using (var scope = host.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            var logger = services.GetRequiredService<ILogger<T>>();
            var context = services.GetRequiredService<T>();

            try
            {
                logger.LogInformation("Migrating database associated with context {DbContextName}", typeof(T).Name);
                context.Database.Migrate();
                logger.LogInformation("Migrated database associated with context {DbContextName}", typeof(T).Name);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while migrating the database used on context {DbContextName}", typeof(T).Name);
            }
        }

        return host;
    }
}