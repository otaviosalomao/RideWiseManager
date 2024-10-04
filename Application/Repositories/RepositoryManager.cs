using ride_wise_api.Application.Repositories.Interfaces;
using ride_wise_api.Infrastructure;

namespace ride_wise_api.Application.Repositories
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly RiseWiseManagerDbContext _context;
        private readonly Lazy<IMotorcycleRepository> _motorcycleRepository;

        public RepositoryManager(RiseWiseManagerDbContext context)
        {
            _context = context;
            _motorcycleRepository = new Lazy<IMotorcycleRepository>(() => new MotorcycleRepository(context));
        }

        public IMotorcycleRepository Motorcycle => _motorcycleRepository.Value;

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
