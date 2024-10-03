using ride_wise_api.Application.Models;
using ride_wise_api.Application.Repositories.Interfaces;
using ride_wise_api.Domain.Models;
using ride_wise_api.Infrastructure;

namespace ride_wise_api.Application.Repositories
{
    public class MotorCycleRepository : RepositoryBase<MotorCycle>, IMotorCycleRepository
    {
        public MotorCycleRepository(RiseWiseManagerDbContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task<MotorCycle> CreateMotorCycle(MotorCycle motorCycle)
        {
            return Create(motorCycle);
        }

        public async Task DeleteMotorCycle(MotorCycle motorCycle)
        {
            Delete(motorCycle);
        }

        public async Task<IEnumerable<MotorCycle>> GetAllMotorCycle()
        {
            return FindAll();
        }

        public async Task<MotorCycle> GetMotorCycleByLicensePlate(string licensePlate)
        {
            return FindByCondition(o => o.LicensePlate == licensePlate).FirstOrDefault();
        }

        public async Task UpdateMotorCycle(MotorCycle motorCycle)
        {
            Update(motorCycle);
        }
    }
}
