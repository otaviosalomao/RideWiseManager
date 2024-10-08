using RideWise.Notification.Application.Repositories.Interfaces;
using RideWise.Notification.Domain.Models;
using RideWise.Notification.Infrastructure;

namespace RideWise.Notification.Application.Repositories
{
    public class MotorcycleNoticeRepository : RepositoryBase<MotorcycleNotice>, IMotorcycleNoticeRepository
    {
        public MotorcycleNoticeRepository(RideWiseNotificationDbContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task<MotorcycleNotice> Create(MotorcycleNotice motorcycle)
        {
            return base.Create(motorcycle);
        }
    }
}
