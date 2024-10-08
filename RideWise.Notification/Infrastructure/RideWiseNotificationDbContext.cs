using Microsoft.EntityFrameworkCore;
using RideWise.Notification.Domain.Models;
using System.Reflection;

namespace RideWise.Notification.Infrastructure
{
    public class RideWiseNotificationDbContext : DbContext
    {
        public DbSet<MotorcycleNotice> MotorcycleNotice { get; set; }    

        public RideWiseNotificationDbContext(DbContextOptions<RideWiseNotificationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
