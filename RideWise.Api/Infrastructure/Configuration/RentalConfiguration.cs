using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RideWise.Api.Domain.Models;

namespace RideWise.Api.Infrastructure.Configuration
{
    public class RentalConfiguration : IEntityTypeConfiguration<Rental>
    {
        public void Configure(EntityTypeBuilder<Rental> builder)
        {
            builder.HasKey(u => new { u.Identification });            
            builder.Property(f => f.Identification).ValueGeneratedOnAdd();
        }
    }
}
