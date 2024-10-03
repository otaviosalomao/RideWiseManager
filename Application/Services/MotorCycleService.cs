using AutoMapper;
using ride_wise_api.Application.Models;
using ride_wise_api.Application.Repositories;
using ride_wise_api.Application.Repositories.Interfaces;
using ride_wise_api.Application.Services.Interfaces;
using ride_wise_api.Domain.Models;

namespace ride_wise_api.Application.Services
{
    public class MotorCycleService : IMotorCycleService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public MotorCycleService(
            IMapper mapper,
            IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public async Task<MotorCycleResult> CreateAsync(MotorCycleRequest request)
        {
            var motorCycle = _mapper.Map<MotorCycle>(request);
            var result = await _repositoryManager.MotorCycle.CreateMotorCycle(motorCycle);
            _repositoryManager.Save();
            return _mapper.Map<MotorCycleResult>(result);
        }

        public async Task<bool> DeleteAsync(string identification)
        {
            var motorCycle = await _repositoryManager.MotorCycle.GetMotorCycleByLicensePlate(identification);
            await _repositoryManager.MotorCycle.DeleteMotorCycle(motorCycle);
            _repositoryManager.Save();
            return true;
        }

        public async Task<IEnumerable<MotorCycleResult>> GetAsync(string licensePlate)
        {
            if (!string.IsNullOrEmpty(licensePlate))
            {
                return _mapper.Map<IEnumerable<MotorCycleResult>>(new List<MotorCycle>() { await _repositoryManager.MotorCycle.GetMotorCycleByLicensePlate(licensePlate) });
            }
            return _mapper.Map<IEnumerable<MotorCycleResult>>(await _repositoryManager.MotorCycle.GetAllMotorCycle());
        }

        public async Task<bool> UpdateAsync(string identification, MotorCycleRequest request)
        {
            return true;
        }
    }
}
