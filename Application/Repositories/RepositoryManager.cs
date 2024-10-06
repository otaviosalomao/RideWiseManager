using ride_wise_api.Application.Repositories.Interfaces;
using ride_wise_api.Infrastructure;

namespace ride_wise_api.Application.Repositories
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly RiseWiseManagerDbContext _context;
        private readonly Lazy<IMotorcycleRepository> _motorcycleRepository;
        private readonly Lazy<IDeliveryAgentRepository> _deliveryAgentRepository;
        private readonly Lazy<IRentalRepository> _rentalRepository;

        public RepositoryManager(RiseWiseManagerDbContext context)
        {
            _context = context;
            _motorcycleRepository = new Lazy<IMotorcycleRepository>(() => new MotorcycleRepository(context));
            _deliveryAgentRepository = new Lazy<IDeliveryAgentRepository>(() => new DeliveryAgentRepository(context));
            _rentalRepository = new Lazy<IRentalRepository>(() => new RentalRepository(context));
        }

        public IMotorcycleRepository Motorcycle => _motorcycleRepository.Value;
        public IDeliveryAgentRepository DeliveryAgent => _deliveryAgentRepository.Value;
        public IRentalRepository Rental => _rentalRepository.Value;

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
