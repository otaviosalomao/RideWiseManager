using AutoMapper;
using RideWise.Api.Application.Models;
using RideWise.Api.Application.Repositories.Interfaces;
using RideWise.Api.Application.Services.Interfaces;
using RideWise.Api.Domain.Models;
using RideWise.Common.Services.Interfaces;

namespace RideWise.Api.Application.Services
{
    public class DeliveryAgentService : IDeliveryAgentService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;
        private readonly ILoggerManager _logger;
        private readonly IDriverLicenseFileManagerService _driverLicenseFileManager;

        public DeliveryAgentService(
            IMapper mapper,
            IRepositoryManager repositoryManager,
            ILoggerManager loggerManager,
            IDriverLicenseFileManagerService driverLicenseFileManager)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
            _logger = loggerManager;
            _driverLicenseFileManager = driverLicenseFileManager;
        }
        public async Task<DeliveryAgentResult> CreateAsync(DeliveryAgentRequest request)
        {
            var filters = new DeliveryAgentFilter(
                identificationDocument: request.Cnpj
                , driverLicenseNumber: request.Numero_cnh);
            var deliveryAgent =
                await _repositoryManager.DeliveryAgent.Get(filters);
            if (deliveryAgent is not null)
            {
                var errorMessage = $"Delivery Agent with identificationDocument {request.Cnpj} already exist";
                _logger.LogError(errorMessage);
                throw new Exception(errorMessage);
            }
            var newDeliveryAgent = _mapper.Map<DeliveryAgent>(request);
            newDeliveryAgent.DriverLicenseFilePath =
                await _driverLicenseFileManager.WriteFile(newDeliveryAgent.DriverLicenseNumber, request.Image_cnh);
            var result = await _repositoryManager.DeliveryAgent.Create(newDeliveryAgent);
            _repositoryManager.Save();
            return _mapper.Map<DeliveryAgentResult>(result);
        }
        public async Task UpdateDriverLicenseImageAsync(string id, string identificationDocumentImage)
        {
            var deliveryAgent =
                await _repositoryManager.DeliveryAgent.Get(new DeliveryAgentFilter(id: id));
            if (deliveryAgent is null)
            {
                var errorMessage = $"Delivery Agent with identification {id} not Found";
                _logger.LogError(errorMessage);
                throw new Exception(errorMessage);
            }
            deliveryAgent.DriverLicenseFilePath =
                await _driverLicenseFileManager.UpdateFile(
                    deliveryAgent.DriverLicenseNumber,
                    identificationDocumentImage,
                    deliveryAgent.DriverLicenseFilePath);
            await _repositoryManager.DeliveryAgent.Update(deliveryAgent);
            _repositoryManager.Save();
        }
    }
}
