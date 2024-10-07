using RideWise.Api.Application.Models;
using RideWise.Api.Domain.Models;

namespace RideWise.Api.Application.Repositories.Interfaces
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
