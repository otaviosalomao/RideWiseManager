using AutoMapper;
using Moq;
using RideWise.Api.Application.Models;
using RideWise.Api.Application.Repositories.Interfaces;
using RideWise.Api.Application.Services;
using RideWise.Api.Application.Services.Interfaces;
using RideWise.Api.Domain.Models;
using RideWise.Common.Services.Interfaces;
using Xunit;

namespace RideWise.Test.Controllers
{
    public class MotorcycleServiceTest
    {
        readonly Mock<ILoggerManager> _logger;
        readonly Mock<IMapper> _mapper;
        readonly Mock<IRepositoryManager> _repositoryManager;
        readonly Mock<IMotorcycleMessageBusProducerService> _messageBusService;
        readonly MotorcycleService _sut;
        public MotorcycleServiceTest()
        {
            _logger = new Mock<ILoggerManager>();
            _mapper = new Mock<IMapper>();
            _repositoryManager = new Mock<IRepositoryManager>();
            _messageBusService = new Mock<IMotorcycleMessageBusProducerService>();
            _sut = new MotorcycleService(_mapper.Object, _repositoryManager.Object, _logger.Object, _messageBusService.Object);
        }

        [Fact]
        public async void MotorcycleService_Create_Successfull()
        {
            var motorcycleRequest = new MotorcycleRequest() { Placa = "123456" };
            var motorcycle = new Motorcycle() { LicensePlate = "123456" };
            var motorcycleResult = new MotorcycleResult() { Placa = "123456" };
            _repositoryManager.Setup(x => x.Motorcycle.Get(It.IsAny<MotorcycleFilter>()));
            _repositoryManager.Setup(x => x.Motorcycle.Create(It.IsAny<Motorcycle>())).Returns(Task.FromResult(motorcycle));
            _mapper.Setup(x => x.Map<Motorcycle>(motorcycleRequest)).Returns(motorcycle);
            _mapper.Setup(x => x.Map<MotorcycleResult>(motorcycle)).Returns(motorcycleResult);
            _repositoryManager.Setup(x => x.Save());

            var result = await _sut.CreateAsync(motorcycleRequest);

            _repositoryManager.Verify(x => x.Save(), Moq.Times.Once);
            result.Equals(motorcycleResult);
        }
        [Fact]
        public async void MotorcycleService_Create_Unsuccessfull()
        {
            var motorcycleRequest = new MotorcycleRequest() { Placa = "123456" };
            var motorcycle = new List<Motorcycle>() { new Motorcycle() { LicensePlate = "123456" } };
            var motorcycleResult = new MotorcycleResult() { Placa = "123456" };
            _repositoryManager.Setup(x => x.Motorcycle.Get(It.IsAny<MotorcycleFilter>())).Returns(Task.FromResult(motorcycle.AsEnumerable()));

            Assert.ThrowsAsync<Exception>(async () => await _sut.CreateAsync(motorcycleRequest));
        }
        [Fact]
        public async void MotorcycleService_UpdateLicensePlate_Successfull()
        {
            var motorcycleRequest = new MotorcycleRequest() { Placa = "123456" };
            var motorcycle = new List<Motorcycle>() { new Motorcycle() { LicensePlate = "123456" } };
            var motorcycleResult = new MotorcycleResult() { Placa = "123456" };
            _repositoryManager.Setup(x => x.Motorcycle.Get(It.IsAny<MotorcycleFilter>())).Returns(Task.FromResult(motorcycle.AsEnumerable()));
            _repositoryManager.Setup(x => x.Motorcycle.Update(It.IsAny<Motorcycle>())).Returns(Task.FromResult(motorcycle));
            _repositoryManager.Setup(x => x.Save());

            await _sut.UpdateLicensePlateAsync(It.IsAny<string>(), It.IsAny<string>());

            _repositoryManager.Verify(x => x.Save(), Moq.Times.Once);
            _repositoryManager.Verify(x => x.Motorcycle.Update(It.IsAny<Motorcycle>()), Moq.Times.Once);
        }
        [Fact]
        public async void MotorcycleService_UpdateLicensePlate_Unsuccessfull()
        {
            var motorcycleRequest = new MotorcycleRequest() { Placa = "123456" };
            var motorcycle = new Motorcycle() { LicensePlate = "123456" };
            var motorcycleResult = new MotorcycleResult() { Placa = "123456" };
            _repositoryManager.Setup(x => x.Motorcycle.Get(It.IsAny<MotorcycleFilter>()));

            Assert.ThrowsAsync<Exception>(async () => await _sut.UpdateLicensePlateAsync(It.IsAny<string>(), It.IsAny<string>()));
        }
        [Fact]
        public async void MotorcycleService_Delete_Successfull()
        {
            var motorcycleRequest = new MotorcycleRequest() { Placa = "123456" };
            var motorcycle = new List<Motorcycle>() { new Motorcycle() { LicensePlate = "123456" } };
            var motorcycleResult = new MotorcycleResult() { Placa = "123456" };
            _repositoryManager.Setup(x => x.Motorcycle.Get(It.IsAny<MotorcycleFilter>())).Returns(Task.FromResult(motorcycle.AsEnumerable()));
            _repositoryManager.Setup(x => x.Motorcycle.Delete(It.IsAny<Motorcycle>())).Returns(Task.FromResult(motorcycle));
            _repositoryManager.Setup(x => x.Save());

            await _sut.DeleteAsync(It.IsAny<string>());

            _repositoryManager.Verify(x => x.Save(), Moq.Times.Once);
            _repositoryManager.Verify(x => x.Motorcycle.Delete(It.IsAny<Motorcycle>()), Moq.Times.Once);
        }
        [Fact]
        public async void MotorcycleService_Delete_Unsuccessfull()
        {
            var motorcycleRequest = new MotorcycleRequest() { Placa = "123456" };
            var motorcycle = new Motorcycle() { LicensePlate = "123456" };
            var motorcycleResult = new MotorcycleResult() { Placa = "123456" };
            _repositoryManager.Setup(x => x.Motorcycle.Get(It.IsAny<MotorcycleFilter>()));

            Assert.ThrowsAsync<Exception>(async () => await _sut.DeleteAsync(It.IsAny<string>()));
        }
    }
}
