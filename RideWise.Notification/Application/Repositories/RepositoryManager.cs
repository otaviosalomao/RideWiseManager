using RideWise.Notification.Application.Repositories.Interfaces;
using RideWise.Notification.Infrastructure;

namespace RideWise.Notification.Application.Repositories
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly RideWiseNotificationDbContext _context;
        private readonly Lazy<IMotorcycleNoticeRepository> _motorcycleNoticeRepository;

        public RepositoryManager(RideWiseNotificationDbContext context)
        {
            _context = context;
            _motorcycleNoticeRepository = new Lazy<IMotorcycleNoticeRepository>(() => new MotorcycleNoticeRepository(context));
        }

        public IMotorcycleNoticeRepository MotorcycleNotice => _motorcycleNoticeRepository.Value;

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
