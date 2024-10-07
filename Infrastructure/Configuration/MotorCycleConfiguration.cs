using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RideWise.Api.Domain.Models;

namespace RideWise.Api.Infrastructure.Configuration
{
    public class MotorcycleConfiguration : IEntityTypeConfiguration<Motorcycle>
    {
        public void Configure(EntityTypeBuilder<Motorcycle> builder)
        {
            builder.HasKey(u => new { u.Id });
            builder.HasIndex(u => u.LicensePlate).IsUnique();
        }
    }
}
