using AutoMapper;
using ride_wise_api.Application.Models;
using ride_wise_api.Application.Repositories.Interfaces;
using ride_wise_api.Application.Services.Interfaces;
using ride_wise_api.Domain.Models;

namespace ride_wise_api.Application.Services
{
    public class DeliveryAgentService : IDeliveryAgentService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;
        readonly ILoggerManager _logger;

        public DeliveryAgentService(
            IMapper mapper,
            IRepositoryManager repositoryManager,
            ILoggerManager loggerManager)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
            _logger = loggerManager;
        }
        public async Task<MotorcycleResult> CreateAsync(DeliveryAgentRequest request)
        {
            var motorcycle =
                await _repositoryManager.DeliveryAgent.Get(new DeliveryAgentFilter(licensePlate: request.Cnpj));
            if (motorcycle.Any())
            {
                var errorMessage = $"Motorcycle with licensePlate {request.Placa} already exist";
                _logger.LogError(errorMessage);
                throw new Exception(errorMessage);
            }
            var motorCycle = _mapper.Map<Motorcycle>(request);
            var result = await _repositoryManager.Motorcycle.Create(motorCycle);
            _repositoryManager.Save();
            return _mapper.Map<MotorcycleResult>(result);
        }
    }
}
