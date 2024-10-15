using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RideWise.Api.Application.Models;
using RideWise.Api.Application.Services.Interfaces;
using RideWise.Api.Controllers;
using RideWise.Common.Services.Interfaces;
using Xunit;

namespace RideWise.Test.Controllers
{
    public class DeliveryAgentControllerTest
    {
        readonly Mock<IDeliveryAgentService> _deliveryAgentService;
        readonly Mock<ILoggerManager> _logger;
        readonly DeliveryAgentController _sut;

        public DeliveryAgentControllerTest()
        {
            _deliveryAgentService = new Mock<IDeliveryAgentService>();
            _logger = new Mock<ILoggerManager>();
            _sut = new DeliveryAgentController(_deliveryAgentService.Object, _logger.Object);
        }

        [Fact]
        public async void DeliveryAgentController_Create_ReturnCreate()
        {
            var deliveryAgentrequest = new Mock<DeliveryAgentRequest>();
            var deliveryAgentresult = new Mock<DeliveryAgentResult>();
            _deliveryAgentService.Setup(x => x.CreateAsync(It.IsAny<DeliveryAgentRequest>())).Returns(Task.FromResult(deliveryAgentresult.Object));

            var result = (CreatedResult)await _sut.Create(deliveryAgentrequest.Object);

            _deliveryAgentService.Verify(x => x.CreateAsync(It.IsAny<DeliveryAgentRequest>()), Moq.Times.Once);
            result.StatusCode.Should().Be(201);
            result.Should().NotBeNull();
        }

        [Fact]
        public async void DeliveryAgentController_Create_ReturnInvalidData()
        {
            var deliveryAgentRequest = new Mock<DeliveryAgentRequest>();
            _deliveryAgentService.Setup(x => x.CreateAsync(It.IsAny<DeliveryAgentRequest>())).Throws(new SystemException("Error Creating delivery agent"));

            var result = (ObjectResult)await _sut.Create(deliveryAgentRequest.Object);

            _deliveryAgentService.Verify(x => x.CreateAsync(It.IsAny<DeliveryAgentRequest>()), Moq.Times.Once);
            result.StatusCode.Should().Be(400);
            result.Value.Equals(new { mensagem = "Dados inválidos" });
        }
        [Fact]
        public async void DeliveryAgentController_UpdateDriverLicenseImage_ReturnUpdate()
        {
            var deliveryAgentDriverLicenseRequest = new Mock<DeliveryAgentDriverLicenseRequest>();
            _deliveryAgentService.Setup(x => x.UpdateDriverLicenseImageAsync(It.IsAny<string>(), It.IsAny<string>()));

            var result = (OkResult)await _sut.UpdateDriverLicenseImage(deliveryAgentDriverLicenseRequest.Object, It.IsAny<string>());

            _deliveryAgentService.Verify(x => x.UpdateDriverLicenseImageAsync(It.IsAny<string>(), It.IsAny<string>()), Moq.Times.Once);
            result.StatusCode.Should().Be(200);
            result.Should().NotBeNull();
        }

        [Fact]
        public async void DeliveryAgentController_UpdateDriverLicenseImage_ReturnInvalidData()
        {
            var deliveryAgentDriverLicenseRequest = new Mock<DeliveryAgentDriverLicenseRequest>();
            _deliveryAgentService.Setup(x => x.UpdateDriverLicenseImageAsync(It.IsAny<string>(), It.IsAny<string>())).Throws(new SystemException("Error Creating delivery agent"));

            var result = (ObjectResult)await _sut.UpdateDriverLicenseImage(deliveryAgentDriverLicenseRequest.Object, It.IsAny<string>());

            _deliveryAgentService.Verify(x => x.UpdateDriverLicenseImageAsync(It.IsAny<string>(), It.IsAny<string>()), Moq.Times.Once);
            result.StatusCode.Should().Be(400);
            result.Value.Equals(new { mensagem = "Dados inválidos" });
        }
    }
}
