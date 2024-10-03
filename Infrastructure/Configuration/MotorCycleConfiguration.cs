using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ride_wise_api.Domain.Models;

namespace ride_wise_api.Infrastructure.Configuration
{
    public class MotorCycleConfiguration : IEntityTypeConfiguration<MotorCycle>
    {
        public void Configure(EntityTypeBuilder<MotorCycle> builder)
        {
            builder.HasKey(u => new { u.LicensePlate });            
        }
    }
}
