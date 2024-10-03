using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ride_wise_api.Domain.Models;
using System.Reflection.Metadata;

namespace ride_wise_api.Infrastructure.Configuration
{
    public class RentalConfiguration : IEntityTypeConfiguration<Rental>
    {
        public void Configure(EntityTypeBuilder<Rental> builder)
        {
            builder.HasKey(u => new { u.Identification });
            builder.HasOne(u => u.MotorCycle)
            .WithOne(u=> u.Rental)
            .HasForeignKey<MotorCycle>(e => e.Identification)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();
            builder.HasOne(u => u.DeliveryAgent)
            .WithOne(u => u.Rental)
            .HasForeignKey<DeliveryAgent>(e => e.Identification)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();
        }
    }
}
