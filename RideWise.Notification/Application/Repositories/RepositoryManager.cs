using RideWise.Notification.Application.Repositories.Interfaces;
using RideWise.Notification.Infrastructure;

namespace RideWise.Notification.Application.Repositories
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly RideWiseNotificationDbContext _context;
        private readonly IMotorcycleNoticeRepository _motorcycleNoticeRepository;

        public RepositoryManager(RideWiseNotificationDbContext context)
        {
            _context = context;
            _motorcycleNoticeRepository = new MotorcycleNoticeRepository(context);
        }

        public IMotorcycleNoticeRepository MotorcycleNotice => _motorcycleNoticeRepository;

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
