using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ride_wise_api.Domain.Models;

namespace ride_wise_api.Infrastructure.Configuration
{
    public class DeliveryAgentConfiguration : IEntityTypeConfiguration<DeliveryAgent>
    {
        public void Configure(EntityTypeBuilder<DeliveryAgent> builder)
        {
            builder.HasKey(u => new { u.DriverLicenseNumber, u.IdentificationDocument });
        }
    }
}
