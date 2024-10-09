using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using RideWise.Api.Application.Models;
using RideWise.Api.Infrastructure;
using RideWise.IntegrationTest.Configurations;
using System.Net.Http.Headers;
using System.Text;
using Xunit;

namespace RideWise.IntegrationTest
{
    public class DeliveryAgentControllerIt : BaseControllerIt, IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private const string validDriverLicenseImageBase64 = "iVBORw0KGgoAAAANSUhEUgAAABgAAAAYCAYAAADgdz34AAAABHNCSVQICAgIfAhkiAAAAAlwSFlzAAAApgAAAKYB3X3/OAAAABl0RVh0U29mdHdhcmUAd3d3Lmlua3NjYXBlLm9yZ5vuPBoAAANCSURBVEiJtZZPbBtFFMZ/M7ubXdtdb1xSFyeilBapySVU8h8OoFaooFSqiihIVIpQBKci6KEg9Q6H9kovIHoCIVQJJCKE1ENFjnAgcaSGC6rEnxBwA04Tx43t2FnvDAfjkNibxgHxnWb2e/u992bee7tCa00YFsffekFY+nUzFtjW0LrvjRXrCDIAaPLlW0nHL0SsZtVoaF98mLrx3pdhOqLtYPHChahZcYYO7KvPFxvRl5XPp1sN3adWiD1ZAqD6XYK1b/dvE5IWryTt2udLFedwc1+9kLp+vbbpoDh+6TklxBeAi9TL0taeWpdmZzQDry0AcO+jQ12RyohqqoYoo8RDwJrU+qXkjWtfi8Xxt58BdQuwQs9qC/afLwCw8tnQbqYAPsgxE1S6F3EAIXux2oQFKm0ihMsOF71dHYx+f3NND68ghCu1YIoePPQN1pGRABkJ6Bus96CutRZMydTl+TvuiRW1m3n0eDl0vRPcEysqdXn+jsQPsrHMquGeXEaY4Yk4wxWcY5V/9scqOMOVUFthatyTy8QyqwZ+kDURKoMWxNKr2EeqVKcTNOajqKoBgOE28U4tdQl5p5bwCw7BWquaZSzAPlwjlithJtp3pTImSqQRrb2Z8PHGigD4RZuNX6JYj6wj7O4TFLbCO/Mn/m8R+h6rYSUb3ekokRY6f/YukArN979jcW+V/S8g0eT/N3VN3kTqWbQ428m9/8k0P/1aIhF36PccEl6EhOcAUCrXKZXXWS3XKd2vc/TRBG9O5ELC17MmWubD2nKhUKZa26Ba2+D3P+4/MNCFwg59oWVeYhkzgN/JDR8deKBoD7Y+ljEjGZ0sosXVTvbc6RHirr2reNy1OXd6pJsQ+gqjk8VWFYmHrwBzW/n+uMPFiRwHB2I7ih8ciHFxIkd/3Omk5tCDV1t+2nNu5sxxpDFNx+huNhVT3/zMDz8usXC3ddaHBj1GHj/As08fwTS7Kt1HBTmyN29vdwAw+/wbwLVOJ3uAD1wi/dUH7Qei66PfyuRj4Ik9is+hglfbkbfR3cnZm7chlUWLdwmprtCohX4HUtlOcQjLYCu+fzGJH2QRKvP3UNz8bWk1qMxjGTOMThZ3kvgLI5AzFfo379UAAAAASUVORK5CYII=";
        public DeliveryAgentControllerIt(CustomWebApplicationFactory<Program> factory) : base(factory)
        {
        }

        [Fact]
        async Task CreateDeliveryAgent_SendingValidDeliveryAgent_ReturnsSuccess()
        {
            InitializeDbTest(feedDb: false);
            var deliveryAgentRequest = new DeliveryAgentRequest()
            {
                Cnpj = "123456789",
                Data_nascimento = DateTime.Now,
                Identificador = "1",
                Image_cnh = validDriverLicenseImageBase64,
                Nome = "Teste",
                Numero_cnh = 123456,
                Tipo_cnh = "A"
            };

            var response = await Post<DeliveryAgentRequest>(deliveryAgentRequest, "entregadores");
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);
        }
        [Fact]
        async Task CreateDeliveryAgent_AlreadyExistsDeliveryAgent_ReturnsBadRequest()
        {
            InitializeDbTest(feedDb: true);
            var deliveryAgentRequest = new DeliveryAgentRequest()
            {
                Cnpj = "1234567893",
                Data_nascimento = DateTime.Now,
                Identificador = "1",
                Image_cnh = validDriverLicenseImageBase64,
                Nome = "Teste",
                Numero_cnh = 12345,
                Tipo_cnh = "A"
            };
            
            var response = await Post<DeliveryAgentRequest>(deliveryAgentRequest, "entregadores");
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
        }
        [Fact]
        async Task CreateDeliveryAgent_InvalidDriverLicense_ReturnsBadRequest()
        {
            InitializeDbTest(feedDb: true);
            var deliveryAgentRequest = new DeliveryAgentRequest()
            {
                Cnpj = "1234567893",
                Data_nascimento = DateTime.Now,
                Identificador = "1",
                Image_cnh = validDriverLicenseImageBase64,
                Nome = "Teste",
                Numero_cnh = 12345,
                Tipo_cnh = "C"
            };

            var response = await Post<DeliveryAgentRequest>(deliveryAgentRequest, "entregadores");
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
        }
        [Fact]
        async Task CreateDeliveryAgent_UpdatingValidDriverLicenseImage_ReturnsSuccessuful()
        {
            InitializeDbTest(feedDb: true);
            var deliveryAgentDriverLicenseRequest = new DeliveryAgentDriverLicenseRequest()
            {
                Imagem_cnh = validDriverLicenseImageBase64
            };            
            var response = await Post<DeliveryAgentDriverLicenseRequest>(deliveryAgentDriverLicenseRequest, "entregadores/1/cnh");
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }
    }
}
