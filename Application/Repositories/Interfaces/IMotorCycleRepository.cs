using ride_wise_api.Application.Models;
using ride_wise_api.Domain.Models;

namespace ride_wise_api.Application.Repositories.Interfaces
{
    public interface IMotorcycleRepository
    {
        Task<Motorcycle> Create(Motorcycle motorcycle);
        Task<bool> Exists(string Identification);
        Task Delete(Motorcycle motorcycle);
        Task Update(Motorcycle motorcycle);
        Task<IEnumerable<Motorcycle>> Get(MotorcycleFilter filters);

    }
}
