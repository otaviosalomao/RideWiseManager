using Microsoft.EntityFrameworkCore;
using ride_wise_api.Domain.Models;
using System.Reflection;

namespace ride_wise_api.Infrastructure
{
    public class RiseWiseManagerDbContext : DbContext
    {
        public DbSet<DeliveryAgent> DeliveryAgents { get; set; }
        public DbSet<Motorcycle> Motorcycles { get; set; }
        public DbSet<Rental> Rentals { get; set; }

        public RiseWiseManagerDbContext(DbContextOptions<RiseWiseManagerDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
