using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RideWise.Api.Domain.Models;

namespace RideWise.Api.Infrastructure.Configuration
{
    public class DeliveryAgentConfiguration : IEntityTypeConfiguration<DeliveryAgent>
    {
        public void Configure(EntityTypeBuilder<DeliveryAgent> builder)
        {
            builder.HasKey(u => new { u.DriverLicenseNumber, u.IdentificationDocument });
        }
    }
}
