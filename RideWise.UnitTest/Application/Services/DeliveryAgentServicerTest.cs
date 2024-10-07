using AutoMapper;
using Moq;
using RideWise.Api.Application.Models;
using RideWise.Api.Application.Repositories.Interfaces;
using RideWise.Api.Application.Services;
using RideWise.Api.Application.Services.Interfaces;
using RideWise.Api.Domain.Models;
using Xunit;

namespace RideWise.Test.Application.Services
{
    public class DeliveryAgentServiceTest
    {
        readonly Mock<ILoggerManager> _logger;
        readonly Mock<IMapper> _mapper;
        readonly Mock<IRepositoryManager> _repositoryManager;        
        private readonly Mock<IDriverLicenseFileManagerService> _driverLicenseFileManager;
        readonly DeliveryAgentService _sut;
        public DeliveryAgentServiceTest()
        {
            _logger = new Mock<ILoggerManager>();
            _mapper = new Mock<IMapper>();
            _repositoryManager = new Mock<IRepositoryManager>();            
            _driverLicenseFileManager = new Mock<IDriverLicenseFileManagerService>();
            _sut = new DeliveryAgentService(_mapper.Object, _repositoryManager.Object, _logger.Object, _driverLicenseFileManager.Object);
        }

        [Fact]
        public async void DeliveryAgentService_Create_Successfull()
        {
            var deliveryAgentRequest = new DeliveryAgentRequest() { Cnpj = "123456"};
            var deliveryAgent = new DeliveryAgent() { IdentificationDocument = "123456" };
            var deliveryAgentResult = new DeliveryAgentResult() { Cnpj = "123456" };
            _repositoryManager.Setup(x => x.DeliveryAgent.Get(It.IsAny<DeliveryAgentFilter>()));
            _driverLicenseFileManager.Setup(x => x.WriteFile(It.IsAny<int>(), It.IsAny<string>())).Returns(Task.FromResult("TESTE"));
            _repositoryManager.Setup(x => x.DeliveryAgent.Create(It.IsAny<DeliveryAgent>())).Returns(Task.FromResult(deliveryAgent));
            _mapper.Setup( x => x.Map<DeliveryAgent>(deliveryAgentRequest)).Returns(deliveryAgent);
            _mapper.Setup(x => x.Map<DeliveryAgentResult>(deliveryAgent)).Returns(deliveryAgentResult);
            _repositoryManager.Setup(x => x.Save());            

            var result = await _sut.CreateAsync(deliveryAgentRequest);

            _repositoryManager.Verify(x => x.Save(), Moq.Times.Once);
            result.Equals(deliveryAgentResult);            
        }
        [Fact]
        public async void DeliveryAgentService_Create_Unsuccessfull()
        {
            var deliveryAgentRequest = new DeliveryAgentRequest() { Cnpj = "123456" };
            var deliveryAgent = new DeliveryAgent() { IdentificationDocument = "123456" };
            var deliveryAgentResult = new DeliveryAgentResult() { Cnpj = "123456" };
            _repositoryManager.Setup(x => x.DeliveryAgent.Get(It.IsAny<DeliveryAgentFilter>())).Returns(Task.FromResult(deliveryAgent));

            Assert.ThrowsAsync<Exception>(async () => await _sut.CreateAsync(deliveryAgentRequest));
        }
        [Fact]
        public async void DeliveryAgentService_UpdateDriverLicenseImage_Successfull()
        {
            var deliveryAgentRequest = new DeliveryAgentRequest() { Cnpj = "123456" };
            var deliveryAgent = new DeliveryAgent() { IdentificationDocument = "123456" };
            var deliveryAgentResult = new DeliveryAgentResult() { Cnpj = "123456" };
            _repositoryManager.Setup(x => x.DeliveryAgent.Get(It.IsAny<DeliveryAgentFilter>())).Returns(Task.FromResult(deliveryAgent));
            _driverLicenseFileManager.Setup(x => x.UpdateFile(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>())).Returns(Task.FromResult("TESTE"));
            _repositoryManager.Setup(x => x.DeliveryAgent.Update(It.IsAny<DeliveryAgent>())).Returns(Task.FromResult(deliveryAgent));            
            _repositoryManager.Setup(x => x.Save());

            await _sut.UpdateDriverLicenseImageAsync(It.IsAny<string>(), It.IsAny<string>());

            _repositoryManager.Verify(x => x.Save(), Moq.Times.Once);
            _repositoryManager.Verify(x => x.DeliveryAgent.Update(It.IsAny<DeliveryAgent>()), Moq.Times.Once);            
        }
        [Fact]
        public async void DeliveryAgentService_UpdateDriverLicenseImage_Unsuccessfull()
        {
            var deliveryAgentRequest = new DeliveryAgentRequest() { Cnpj = "123456" };
            var deliveryAgent = new DeliveryAgent() { IdentificationDocument = "123456" };
            var deliveryAgentResult = new DeliveryAgentResult() { Cnpj = "123456" };
            _repositoryManager.Setup(x => x.DeliveryAgent.Get(It.IsAny<DeliveryAgentFilter>()));

            Assert.ThrowsAsync<Exception>(async () => await _sut.CreateAsync(deliveryAgentRequest));
        }
    }
}

