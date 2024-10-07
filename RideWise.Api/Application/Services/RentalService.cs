using AutoMapper;
using RideWise.Api.Application.Models;
using RideWise.Api.Application.Repositories.Interfaces;
using RideWise.Api.Application.Services.Interfaces;
using RideWise.Api.Domain.Models;
using RideWise.Api.Domain.Services.Interfaces;

namespace RideWise.Api.Application.Services
{
    public class RentalService : IRentalService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;
        readonly ILoggerManager _logger;  
        readonly IRentService _rentService;

        public RentalService(
            IMapper mapper,
            IRepositoryManager repositoryManager,
            ILoggerManager loggerManager,
            IRentService rentService)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
            _logger = loggerManager;    
            _rentService = rentService;
        }
        public async Task<RentalResult> CreateAsync(RentalRequest request)
        {
            var rentalFilters = new RentalFilter(
                deliveryAgentIdentification: request.Entregador_id,
                motorcycleIdentification: request.Moto_id);
            var rental =
                await _repositoryManager.Rental.Get(rentalFilters);            
            var existMotorcycle = await _repositoryManager.Motorcycle.Exists(request.Moto_id);
            var existDeliveryAgent = await _repositoryManager.DeliveryAgent.Exists(request.Entregador_id);
            if (!existDeliveryAgent)
            {
                var errorMessage = $"Delivery agent with Id {request.Entregador_id} not found";
                _logger.LogError(errorMessage);
                throw new Exception(errorMessage);
            }
            if (!existMotorcycle)
            {
                var errorMessage = $"Motorcycle with Id {request.Moto_id} not found";
                _logger.LogError(errorMessage);
                throw new Exception(errorMessage);
            }
            if (rental is not null)
            {
                var errorMessage = $"Rental with Id {request.Entregador_id} already exist";
                _logger.LogError(errorMessage);
                throw new Exception(errorMessage);
            }
            var newRental = _mapper.Map<Rental>(request);
            newRental.DailyValue = _rentService.RentValue(newRental.PlanNumber);
            var result = await _repositoryManager.Rental.Create(newRental);            
            _repositoryManager.Save();
            return _mapper.Map<RentalResult>(result);
        }

        public async Task<RentalResult> GetAsync(RentalFilter filters)
        {
            var rental = await _repositoryManager.Rental.Get(filters);
            return _mapper.Map<RentalResult>(rental);
        }

        public async Task<bool> UpdateDevolutionDateAsync(string identification, DateTime devolutionDate)
        {
            var filter = new RentalFilter(identification: identification);
            var rental = await _repositoryManager.Rental.Get(filter);
            if (rental is null)
            {
                var errorMessage = $"Rental not found for identification {identification}";
                _logger.LogError(errorMessage);
                throw new Exception(errorMessage);
            }

            rental.EndDate = devolutionDate;
            await _repositoryManager.Rental.Update(rental);
            _repositoryManager.Save();
            return true;
        }
    }
}
