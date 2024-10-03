using ride_wise_api.Application.Models;
using ride_wise_api.Domain.Models;

namespace ride_wise_api.Application.Repositories.Interfaces
{
    public interface IMotorCycleRepository
    {
        Task<MotorCycle> CreateMotorCycle(MotorCycle motorCycle);
        Task DeleteMotorCycle(MotorCycle motorCycle);
        Task UpdateMotorCycle(MotorCycle motorCycle);
        Task<IEnumerable<MotorCycle>> GetAllMotorCycle();
        Task<MotorCycle> GetMotorCycleByLicensePlate(string licensePlate);

    }
}
