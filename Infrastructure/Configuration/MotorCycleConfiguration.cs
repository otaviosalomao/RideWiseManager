using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ride_wise_api.Domain.Models;

namespace ride_wise_api.Infrastructure.Configuration
{
    public class MotorcycleConfiguration : IEntityTypeConfiguration<Motorcycle>
    {
        public void Configure(EntityTypeBuilder<Motorcycle> builder)
        {
            builder.HasKey(u => new { u.LicensePlate });
        }
    }
}
