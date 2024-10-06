using AutoMapper;
using ride_wise_api.Application.Models;
using ride_wise_api.Application.Repositories.Interfaces;
using ride_wise_api.Application.Services.Interfaces;
using ride_wise_api.Domain.Models;

namespace ride_wise_api.Application.Services
{
    public class RentalService : IRentalService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;
        readonly ILoggerManager _logger;        

        public RentalService(
            IMapper mapper,
            IRepositoryManager repositoryManager,
            ILoggerManager loggerManager)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
            _logger = loggerManager;            
        }
        public async Task<RentalResult> CreateAsync(RentalRequest request)
        {
            var rental =
                await _repositoryManager.Rental.Get(new RentalFilter(deliveryAgentIdentification: request.Entregador_id));
            if (rental is not null)
            {
                var errorMessage = $"Rental with Id {request.Entregador_id} already exist";
                _logger.LogError(errorMessage);
                throw new Exception(errorMessage);
            }
            var newRental = _mapper.Map<Rental>(request);
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
