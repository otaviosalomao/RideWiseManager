using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RideWise.Api.Application.Models;
using RideWise.Api.Application.Services;
using RideWise.Api.Application.Services.Interfaces;
using RideWise.Api.Controllers;
using Xunit;

namespace RideWise.Test.Controllers
{
    public class MotorcycleControllerTest
    {
        readonly Mock<IMotorcycleService> _motorcycleService;
        readonly Mock<ILoggerManager> _logger;
        readonly MotorcyclesController _sut;

        public MotorcycleControllerTest()
        {
            _motorcycleService = new Mock<IMotorcycleService>();
            _logger = new Mock<ILoggerManager>();
            _sut = new MotorcyclesController(_motorcycleService.Object, _logger.Object);
        }

        [Fact]
        public async void MotorcyclesController_GetAll_NotFound()
        {            
            var motorcycleResult = Enumerable.Empty<MotorcycleResult>();
            _motorcycleService.Setup(x => x.GetAsync(It.IsAny<MotorcycleFilter>())).Returns(Task.FromResult(motorcycleResult));

            var result = (ObjectResult)await _sut.GetAll(It.IsAny<string>());

            _motorcycleService.Verify(x => x.GetAsync(It.IsAny<MotorcycleFilter>()), Moq.Times.Once);
            result.StatusCode.Should().Be(404);
            result.Value.Equals(new { mensagem = "Moto(s) não encontrada(s)" });
        }
        [Fact]
        public async void MotorcyclesController_GetAll_Found()
        {
            var motorcycleResult = new List<MotorcycleResult>();
            motorcycleResult.Add(new MotorcycleResult());
            _motorcycleService.Setup(x => x.GetAsync(It.IsAny<MotorcycleFilter>())).Returns(Task.FromResult(motorcycleResult.AsEnumerable()));

            var result = (ObjectResult)await _sut.GetAll(It.IsAny<string>());

            _motorcycleService.Verify(x => x.GetAsync(It.IsAny<MotorcycleFilter>()), Moq.Times.Once);
            result.StatusCode.Should().Be(200);
            result.Should().NotBeNull();
        }
        [Fact]
        public async void MotorcyclesController_GetAll_InvalidData()
        {
            var motorcycleResult = new List<MotorcycleResult>();
            motorcycleResult.Add(new MotorcycleResult());
            _motorcycleService.Setup(x => x.GetAsync(It.IsAny<MotorcycleFilter>())).Throws(new SystemException("Error Getting motorcycle"));

            var result = (ObjectResult)await _sut.GetAll(It.IsAny<string>());

            _motorcycleService.Verify(x => x.GetAsync(It.IsAny<MotorcycleFilter>()), Moq.Times.Once);
            result.StatusCode.Should().Be(400);
            result.Value.Equals(new { mensagem = "Dados inválidos" });
        }
        [Fact]
        public async void MotorcyclesController_GetByIdentification_NotFound()
        {
            var motorcycleResult = Enumerable.Empty<MotorcycleResult>();
            _motorcycleService.Setup(x => x.GetAsync(It.IsAny<MotorcycleFilter>())).Returns(Task.FromResult(motorcycleResult));

            var result = (ObjectResult)await _sut.GetByIdentification(It.IsAny<string>());

            _motorcycleService.Verify(x => x.GetAsync(It.IsAny<MotorcycleFilter>()), Moq.Times.Once);
            result.StatusCode.Should().Be(404);
            result.Value.Equals(new { mensagem = "Moto(s) não encontrada(s)" });
        }
        [Fact]
        public async void MotorcyclesController_GetByIdentification_Found()
        {
            var motorcycleResult = new List<MotorcycleResult>();
            motorcycleResult.Add(new MotorcycleResult());
            _motorcycleService.Setup(x => x.GetAsync(It.IsAny<MotorcycleFilter>())).Returns(Task.FromResult(motorcycleResult.AsEnumerable()));

            var result = (ObjectResult)await _sut.GetByIdentification(It.IsAny<string>());

            _motorcycleService.Verify(x => x.GetAsync(It.IsAny<MotorcycleFilter>()), Moq.Times.Once);
            result.StatusCode.Should().Be(200);
            result.Should().NotBeNull();
        }
        [Fact]
        public async void MotorcyclesController_GetByIdentification_InvalidData()
        {
            var motorcycleResult = new List<MotorcycleResult>();
            motorcycleResult.Add(new MotorcycleResult());
            _motorcycleService.Setup(x => x.GetAsync(It.IsAny<MotorcycleFilter>())).Throws(new SystemException("Error Getting motorcycle"));

            var result = (ObjectResult)await _sut.GetByIdentification(It.IsAny<string>());

            _motorcycleService.Verify(x => x.GetAsync(It.IsAny<MotorcycleFilter>()), Moq.Times.Once);
            result.StatusCode.Should().Be(400);
            result.Value.Equals(new { mensagem = "Dados inválidos" });
        }
        [Fact]
        public async void MotorcycleController_Create_ReturnCreate()
        {
            var motorcycleRequest = new Mock<MotorcycleRequest>();
            var motorcycleResult = new Mock<MotorcycleResult>();
            _motorcycleService.Setup(x => x.CreateAsync(It.IsAny<MotorcycleRequest>())).Returns(Task.FromResult(motorcycleResult.Object));

            var result = (CreatedResult)await _sut.Create(motorcycleRequest.Object);

            _motorcycleService.Verify(x => x.CreateAsync(It.IsAny<MotorcycleRequest>()), Moq.Times.Once);
            result.StatusCode.Should().Be(201);
            result.Should().NotBeNull();
        }

        [Fact]
        public async void MotorcycleController_Create_ReturnInvalidData()
        {
            var motorcycleRequest = new Mock<MotorcycleRequest>();
            _motorcycleService.Setup(x => x.CreateAsync(It.IsAny<MotorcycleRequest>())).Throws(new SystemException("Error Creating motorcycle"));

            var result = (ObjectResult)await _sut.Create(motorcycleRequest.Object);

            _motorcycleService.Verify(x => x.CreateAsync(It.IsAny<MotorcycleRequest>()), Moq.Times.Once);
            result.StatusCode.Should().Be(400);
            result.Value.Equals(new { mensagem = "Dados inválidos" });
        }
        [Fact]
        public async void MotorcycleController_UpdateLicensePlate_ReturnUpdate()
        {
            var motorcycleLicensePlate = new MotorcycleLicensePlate();            
            _motorcycleService.Setup(x => x.UpdateLicensePlateAsync(It.IsAny<string>(), It.IsAny<string>()));

            var result = (OkObjectResult)await _sut.UpdateLicensePlate(It.IsAny<string>(), motorcycleLicensePlate);

            _motorcycleService.Verify(x => x.UpdateLicensePlateAsync(It.IsAny<string>(), It.IsAny<string>()), Moq.Times.Once);
            result.StatusCode.Should().Be(200);
            result.Should().NotBeNull();
        }

        [Fact]
        public async void MotorcycleController_UpdateLicensePlate_ReturnInvalidData()
        {
            var motorcycleLicensePlate = new Mock<MotorcycleLicensePlate>();
            _motorcycleService.Setup(x => x.UpdateLicensePlateAsync(It.IsAny<string>(), It.IsAny<string>())).Throws(new SystemException("Error Creating motorcycle"));

            var result = (ObjectResult)await _sut.UpdateLicensePlate(It.IsAny<string>(), motorcycleLicensePlate.Object);

            _motorcycleService.Verify(x => x.UpdateLicensePlateAsync(It.IsAny<string>(), It.IsAny<string>()), Moq.Times.Once);
            result.StatusCode.Should().Be(400);
            result.Value.Equals(new { mensagem = "Dados inválidos" });
        }
        [Fact]
        public async void MotorcycleController_Delete_ReturnDelete()
        {            
            _motorcycleService.Setup(x => x.DeleteAsync(It.IsAny<string>()));

            var result = (OkResult)await _sut.Delete(It.IsAny<string>());

            _motorcycleService.Verify(x => x.DeleteAsync(It.IsAny<string>()), Moq.Times.Once);
            result.StatusCode.Should().Be(200);
            result.Should().NotBeNull();
        }

        [Fact]
        public async void MotorcycleController_Delete_ReturnInvalidData()
        {         
            _motorcycleService.Setup(x => x.DeleteAsync(It.IsAny<string>())).Throws(new SystemException("Error Creating motorcycle"));

            var result = (ObjectResult)await _sut.Delete(It.IsAny<string>());

            _motorcycleService.Verify(x => x.DeleteAsync(It.IsAny<string>()), Moq.Times.Once);
            result.StatusCode.Should().Be(400);
            result.Value.Equals(new { mensagem = "Dados inválidos" });
        }
    }
}
