using ride_wise_api.Application.Models;
using ride_wise_api.Domain.Models;

namespace ride_wise_api.Application.Repositories.Interfaces
{
    public interface IMotorcycleRepository
    {
        Task<Motorcycle> Create(Motorcycle motorCycle);
        Task Delete(Motorcycle motorCycle);
        Task Update(Motorcycle motorCycle);
        Task<IEnumerable<Motorcycle>> Get(MotorcycleFilter filters);

    }
}
