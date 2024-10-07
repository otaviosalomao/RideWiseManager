using Microsoft.EntityFrameworkCore;
using RideWise.Api.Domain.Models;
using System.Reflection;

namespace RideWise.Api.Infrastructure
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
