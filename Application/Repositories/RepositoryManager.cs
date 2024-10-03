using ride_wise_api.Application.Repositories.Interfaces;
using ride_wise_api.Infrastructure;

namespace ride_wise_api.Application.Repositories
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly RiseWiseManagerDbContext _context;
        private readonly Lazy<IMotorCycleRepository> _motorCycleRepository;

        public RepositoryManager(RiseWiseManagerDbContext context)
        {
            _context = context;
            _motorCycleRepository = new Lazy<IMotorCycleRepository>(() => new MotorCycleRepository(context));
        }

        public IMotorCycleRepository MotorCycle => _motorCycleRepository.Value;

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
