using AutoMapper;
using RideWise.Api.Application.Models;
using RideWise.Api.Application.Repositories.Interfaces;
using RideWise.Api.Application.Services.Interfaces;
using RideWise.Api.Domain.Models;

namespace RideWise.Api.Application.Services
{
    public class MotorcycleService : IMotorcycleService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;
        readonly ILoggerManager _logger;
        readonly IMotorcycleMessageBusProducerService _messageBusProducer;

        public MotorcycleService(
            IMapper mapper,
            IRepositoryManager repositoryManager,
            ILoggerManager loggerManager,
            IMotorcycleMessageBusProducerService messageBusProducer)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
            _logger = loggerManager;
            _messageBusProducer = messageBusProducer;
        }

        public async Task<MotorcycleResult> CreateAsync(MotorcycleRequest request)
        {
            var motorcycle =
                await _repositoryManager.Motorcycle.Get(new MotorcycleFilter(licensePlate: request.Placa));
            if (motorcycle.Any())
            {
                var errorMessage = $"Motorcycle with licensePlate {request.Placa} already exist";
                _logger.LogError(errorMessage);
                throw new Exception(errorMessage);
            }
            var newMotorcycle = _mapper.Map<Motorcycle>(request);
            var result = await _repositoryManager.Motorcycle.Create(newMotorcycle);
            await _messageBusProducer.Publish(result);
            _repositoryManager.Save();
            return _mapper.Map<MotorcycleResult>(result);
        }

        public async Task<bool> DeleteAsync(string identification)
        {
            var motorcycle =
                await _repositoryManager.Motorcycle.Get(new MotorcycleFilter(identification: identification));
            if (!motorcycle.Any())
            {
                _logger.LogError($"Motorcycle not found for identification {identification}");
                return false;
            }
            await _repositoryManager.Motorcycle.Delete(motorcycle.FirstOrDefault());
            _repositoryManager.Save();
            return true;
        }

        public async Task<IEnumerable<MotorcycleResult>> GetAsync(MotorcycleFilter filters)
        {
            var motorcycles = await _repositoryManager.Motorcycle.Get(filters);
            return _mapper.Map<IEnumerable<MotorcycleResult>>(motorcycles);
        }

        public async Task<bool> UpdateLicensePlateAsync(string identification, string licensePlate)
        {
            var filter = new MotorcycleFilter(identification: identification);
            var motorcycle =
                await _repositoryManager.Motorcycle.Get(filter);
            if (!motorcycle.Any())
            {
                var errorMessage = $"Motorcycle not found for identification {identification}";
                _logger.LogError(errorMessage);
                throw new Exception(errorMessage);
            }
            var updateMotorcycle = motorcycle.FirstOrDefault();
            updateMotorcycle.LicensePlate = licensePlate;
            await _repositoryManager.Motorcycle.Update(updateMotorcycle);
            _repositoryManager.Save();
            return true;
        }
    }
}
