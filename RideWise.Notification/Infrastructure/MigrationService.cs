using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RideWise.Notification.Infrastructure;

namespace RideWise.Notification.Infrastructure
{
    public static class MigrationService
    {
        public static void InitializaMigration(this ServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetService<RideWiseNotificationDbContext>();
            context.Database.Migrate();
        }
    }
}