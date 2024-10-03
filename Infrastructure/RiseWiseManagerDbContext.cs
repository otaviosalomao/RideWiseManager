using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Reflection;
using System;
using ride_wise_api.Domain.Models;

namespace ride_wise_api.Infrastructure
{
    public class RiseWiseManagerDbContext : DbContext
    {
        public DbSet<DeliveryAgent> DeliveryAgents { get; set; }
        public DbSet<MotorCycle> MotorCycles { get; set; }
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
