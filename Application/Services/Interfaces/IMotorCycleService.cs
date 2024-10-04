using ride_wise_api.Application.Models;

namespace ride_wise_api.Application.Services.Interfaces
{
    public interface IMotorcycleService
    {
        Task<IEnumerable<MotorcycleResult>> GetAsync(MotorcycleFilter filters);
        Task<MotorcycleResult> CreateAsync(MotorcycleRequest request);
        Task<bool> UpdateLicensePlateAsync(string identification, MotorcycleLicensePlate licensePlate);
        Task<bool> DeleteAsync(string identification);
    }
}
