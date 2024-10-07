using AutoMapper;
using Moq;
using RideWise.Api.Application.Models;
using RideWise.Api.Application.Repositories.Interfaces;
using RideWise.Api.Application.Services;
using RideWise.Api.Application.Services.Interfaces;
using RideWise.Api.Domain.Models;
using RideWise.Api.Domain.Services.Interfaces;
using Xunit;

namespace RideWise.Test.Services
{
    public class RentalServiceTest
    {
        readonly Mock<ILoggerManager> _logger;
        readonly Mock<IMapper> _mapper;
        readonly Mock<IRepositoryManager> _repositoryManager;
        readonly Mock<IRentService> _rentService;
        readonly RentalService _sut;
        public RentalServiceTest()
        {
            _logger = new Mock<ILoggerManager>();
            _mapper = new Mock<IMapper>();
            _repositoryManager = new Mock<IRepositoryManager>();
            _rentService = new Mock<IRentService>();
            _sut = new RentalService(_mapper.Object, _repositoryManager.Object, _logger.Object, _rentService.Object);
        }
        [Fact]
        public async void RentalService_Create_Successfull()
        {
            var rentalRequest = new RentalRequest() { Entregador_id = "123456", Moto_id = "123456" };
            var rental = new Rental() { DeliveryAgentIdentification = "123456", MotorcycleIdentification = "123456" };
            var rentalResult = new RentalResult() { Entregador_id = "123456", Moto_id = "123456" };
            _repositoryManager.Setup(x => x.Rental.Get(It.IsAny<RentalFilter>()));
            _repositoryManager.Setup(x => x.Motorcycle.Exists(It.IsAny<string>())).Returns(Task.FromResult(true));
            _repositoryManager.Setup(x => x.DeliveryAgent.Exists(It.IsAny<string>())).Returns(Task.FromResult(true));
            _repositoryManager.Setup(x => x.Rental.Create(It.IsAny<Rental>())).Returns(Task.FromResult(rental));
            _mapper.Setup(x => x.Map<Rental>(rentalRequest)).Returns(rental);
            _mapper.Setup(x => x.Map<RentalResult>(rental)).Returns(rentalResult);
            _repositoryManager.Setup(x => x.Save());

            var result = await _sut.CreateAsync(rentalRequest);

            _repositoryManager.Verify(x => x.Save(), Moq.Times.Once);
            result.Equals(rentalResult);
        }
        [Fact]
        public async void RentalService_Create_Unsuccessfull()
        {
            var rentalRequest = new RentalRequest() { Entregador_id = "123456", Moto_id = "123456" };
            var rental = new Rental() { DeliveryAgentIdentification = "123456", MotorcycleIdentification = "123456" };
            var rentalResult = new RentalResult() { Entregador_id = "123456", Moto_id = "123456" };
            _repositoryManager.Setup(x => x.Rental.Get(It.IsAny<RentalFilter>())).Returns(Task.FromResult(rental));

            Assert.ThrowsAsync<Exception>(async () => await _sut.CreateAsync(rentalRequest));
        }
        [Fact]
        public async void RentalService_UpdateLicensePlate_Successfull()
        {
            var rentalRequest = new RentalRequest() { Entregador_id = "123456", Moto_id = "123456" };
            var rental = new Rental() { DeliveryAgentIdentification = "123456", MotorcycleIdentification = "123456" };
            var rentalResult = new RentalResult() { Entregador_id = "123456", Moto_id = "123456" };
            _repositoryManager.Setup(x => x.Rental.Get(It.IsAny<RentalFilter>())).Returns(Task.FromResult(rental));
            _repositoryManager.Setup(x => x.Rental.Update(It.IsAny<Rental>())).Returns(Task.FromResult(rental));
            _repositoryManager.Setup(x => x.Save());

            await _sut.UpdateDevolutionDateAsync(It.IsAny<string>(), It.IsAny<DateTime>());

            _repositoryManager.Verify(x => x.Save(), Moq.Times.Once);
            _repositoryManager.Verify(x => x.Rental.Update(It.IsAny<Rental>()), Moq.Times.Once);
        }
        [Fact]
        public async void RentalService_UpdateLicensePlate_Unsuccessfull()
        {
            var rentalRequest = new RentalRequest() { Entregador_id = "123456", Moto_id = "123456" };
            var rental = new Rental() { DeliveryAgentIdentification = "123456", MotorcycleIdentification = "123456" };
            var rentalResult = new RentalResult() { Entregador_id = "123456", Moto_id = "123456" };
            _repositoryManager.Setup(x => x.Rental.Get(It.IsAny<RentalFilter>()));

            Assert.ThrowsAsync<Exception>(async () => await _sut.UpdateDevolutionDateAsync(It.IsAny<string>(), It.IsAny<DateTime>()));
        }        
    }
}
