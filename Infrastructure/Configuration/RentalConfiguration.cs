using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ride_wise_api.Domain.Models;

namespace ride_wise_api.Infrastructure.Configuration
{
    public class RentalConfiguration : IEntityTypeConfiguration<Rental>
    {
        public void Configure(EntityTypeBuilder<Rental> builder)
        {
            builder.HasKey(u => new { u.Identification });
            builder.HasOne(u => u.Motorcycle);                        
            builder.HasOne(u => u.DeliveryAgent);
        }
    }
}
