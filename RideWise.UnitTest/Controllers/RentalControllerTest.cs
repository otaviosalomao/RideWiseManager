using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RideWise.Api.Application.Models;
using RideWise.Api.Application.Services.Interfaces;
using RideWise.Api.Controllers;
using Xunit;

namespace RideWise.Test.Controllers
{
    public class RentalContollerTest
    {
        readonly Mock<IRentalService> _rentalService;
        readonly Mock<ILoggerManager> _logger;
        readonly RentalController _sut;

        public RentalContollerTest()
        {
            _rentalService = new Mock<IRentalService>();
            _logger = new Mock<ILoggerManager>();
            _sut = new RentalController(_rentalService.Object, _logger.Object);
        }
        [Fact]
        public async void RentalController_Create_ReturnCreate()
        {
            var rentalRequest = new Mock<RentalRequest>();
            var rentalResult = new Mock<RentalResult>();
            _rentalService.Setup(x => x.CreateAsync(It.IsAny<RentalRequest>())).Returns(Task.FromResult(rentalResult.Object));

            var result = (CreatedResult)await _sut.Create(rentalRequest.Object);

            _rentalService.Verify(x => x.CreateAsync(It.IsAny<RentalRequest>()), Moq.Times.Once);
            result.StatusCode.Should().Be(201);
            result.Should().NotBeNull();
        }

        [Fact]
        public async void RentalController_Create_ReturnInvalidData()
        {
            var rentalRequest = new Mock<RentalRequest>();
            _rentalService.Setup(x => x.CreateAsync(It.IsAny<RentalRequest>())).Throws(new SystemException("Error Creating Rental"));

            var result = (ObjectResult)await _sut.Create(rentalRequest.Object);

            _rentalService.Verify(x => x.CreateAsync(It.IsAny<RentalRequest>()), Moq.Times.Once);
            result.StatusCode.Should().Be(400);
            result.Value.Equals(new { mensagem = "Dados inválidos" });
        }
        [Fact]
        public async void MotorcyclesController_GetByIdentification_NotFound()
        {
            _rentalService.Setup(x => x.GetAsync(It.IsAny<RentalFilter>()));

            var result = (ObjectResult)await _sut.GetByIdentification(It.IsAny<string>());

            _rentalService.Verify(x => x.GetAsync(It.IsAny<RentalFilter>()), Moq.Times.Once);
            result.StatusCode.Should().Be(404);
            result.Value.Equals(new { mensagem = "Locação não encontrada" });
        }
        [Fact]
        public async void MotorcyclesController_GetByIdentification_Found()
        {
            var rentalResult = new RentalResult();
            _rentalService.Setup(x => x.GetAsync(It.IsAny<RentalFilter>())).Returns(Task.FromResult(rentalResult));

            var result = (ObjectResult)await _sut.GetByIdentification(It.IsAny<string>());

            _rentalService.Verify(x => x.GetAsync(It.IsAny<RentalFilter>()), Moq.Times.Once);
            result.StatusCode.Should().Be(200);
            result.Should().NotBeNull();
        }
        [Fact]
        public async void MotorcyclesController_GetByIdentification_InvalidData()
        {
            var rentalResult = new List<RentalResult>();
            rentalResult.Add(new RentalResult());
            _rentalService.Setup(x => x.GetAsync(It.IsAny<RentalFilter>())).Throws(new SystemException("Error Getting Rental"));

            var result = (ObjectResult)await _sut.GetByIdentification(It.IsAny<string>());

            _rentalService.Verify(x => x.GetAsync(It.IsAny<RentalFilter>()), Moq.Times.Once);
            result.StatusCode.Should().Be(400);
            result.Value.Equals(new { mensagem = "Dados inválidos" });
        }
        [Fact]
        public async void MotorcycleController_UpdateDevolutionDate_ReturnUpdate()
        {
            var rentalDevolutionDate = new RentalDevolutionDate();
            _rentalService.Setup(x => x.UpdateDevolutionDateAsync(It.IsAny<string>(), It.IsAny<DateTime>()));

            var result = (OkObjectResult)await _sut.UpdateByDevolutionDate(It.IsAny<string>(), rentalDevolutionDate);

            _rentalService.Verify(x => x.UpdateDevolutionDateAsync(It.IsAny<string>(), It.IsAny<DateTime>()), Moq.Times.Once);
            result.StatusCode.Should().Be(200);
            result.Should().NotBeNull();
        }

        [Fact]
        public async void MotorcycleController_UpdateDevolutionDate_ReturnInvalidData()
        {
            var rentalDevolutionDate = new RentalDevolutionDate();
            _rentalService.Setup(x => x.UpdateDevolutionDateAsync(It.IsAny<string>(), It.IsAny<DateTime>())).Throws(new SystemException("Error updating rental"));

            var result = (ObjectResult)await _sut.UpdateByDevolutionDate(It.IsAny<string>(), rentalDevolutionDate);

            _rentalService.Verify(x => x.UpdateDevolutionDateAsync(It.IsAny<string>(), It.IsAny<DateTime>()), Moq.Times.Once);
            result.StatusCode.Should().Be(400);
            result.Value.Equals(new { mensagem = "Dados inválidos" });
        }
    }
}
