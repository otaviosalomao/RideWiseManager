using ride_wise_api.Application.Models;

namespace ride_wise_api.Application.Services.Interfaces
{
    public interface IMotorCycleService
    {
        Task<IEnumerable<MotorCycleResult>> GetAsync(string LicensePlate);
        Task<MotorCycleResult> CreateAsync(MotorCycleRequest request);
        Task<bool> UpdateAsync(string identificationDocument, MotorCycleRequest request);
        Task<bool> DeleteAsync(string identification);
    }
}
