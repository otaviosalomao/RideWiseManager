using RideWise.Api.Application.Models;

namespace RideWise.Api.Application.Services.Interfaces
{
    public interface IMotorcycleService
    {
        Task<IEnumerable<MotorcycleResult>> GetAsync(MotorcycleFilter filters);
        Task<MotorcycleResult> CreateAsync(MotorcycleRequest request);
        Task<bool> UpdateLicensePlateAsync(string identification, string licensePlate);
        Task<bool> DeleteAsync(string identification);
    }
}
