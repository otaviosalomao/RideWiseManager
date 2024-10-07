using Microsoft.EntityFrameworkCore;
using RideWise.Api.Domain.Models;
using System.Reflection;

namespace RideWise.Api.Infrastructure
{
    public class RideWiseApiDbContext : DbContext
    {
        public DbSet<DeliveryAgent> DeliveryAgents { get; set; }
        public DbSet<Motorcycle> Motorcycles { get; set; }
        public DbSet<Rental> Rentals { get; set; }

        public RideWiseApiDbContext(DbContextOptions<RideWiseApiDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
