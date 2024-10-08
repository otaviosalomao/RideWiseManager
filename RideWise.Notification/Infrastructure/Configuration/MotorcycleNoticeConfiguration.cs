using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RideWise.Notification.Domain.Models;

namespace RideWise.Notification.Infrastructure.Configuration
{
    public class MotorcycleNoticeConfiguration : IEntityTypeConfiguration<MotorcycleNotice>
    {
        public void Configure(EntityTypeBuilder<MotorcycleNotice> builder)
        {
            builder.HasKey(u => new { u.Id });
            builder.Property(f => f.Id).ValueGeneratedOnAdd();
        }
    }
}
