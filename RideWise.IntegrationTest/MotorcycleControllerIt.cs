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
    public class MotorcycleControllerIt : BaseControllerIt, IClassFixture<CustomWebApplicationFactory<Program>>
    {
        public MotorcycleControllerIt(CustomWebApplicationFactory<Program> factory) : base(factory)
        {
        }

        [Fact]
        async Task GetMotorcycles_MotorcyclesExists_ReturnsSuccess()
        {
            var response = await _httpClient.GetAsync("/motos");
            var result = await response.Content.ReadAsStringAsync();
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            result.Should().NotBeEmpty();
        }
        [Fact]
        async Task GetMotorcycle_MotorcycleExist_ReturnsSuccessByLicensePlate()
        {
            var response = await _httpClient.GetAsync("/motos?placa=GBA-1G95");
            var result = await response.Content.ReadAsStringAsync();
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            result.Should().NotBeEmpty();
        }
        [Fact]
        async Task GetMotorcycle_MotorcycleNotExist_ReturnsNotFound()
        {            
            var response = await _httpClient.GetAsync("/motos?placa=23");
            var result = await response.Content.ReadAsStringAsync();
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
        }
        [Fact]
        async Task GetMotorcycle_MotorcycleExist_ReturnsSuccessById()
        {
            var response = await _httpClient.GetAsync("/motos/1");
            var result = await response.Content.ReadAsStringAsync();
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            result.Should().NotBeEmpty();
        }
        [Fact]
        async Task CreateMotorcycle_ValidMotorcycleRequest_ReturnsSuccess()
        {
            var motorcycleRequest = new MotorcycleRequest()
            {
                Ano = 2024,
                Identificador = "5",
                Modelo = "Harley",
                Placa = "GBA-1G954"
            };

            var response = await Post<MotorcycleRequest>(motorcycleRequest, "motos");
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);
        }
        [Fact]
        async Task CreateMotorcycle_AlreadyCreatedMotorcycleRequest_ReturnsBadRequest()
        {
            var motorcycleRequest = new MotorcycleRequest()
            {
                Ano = 2024,
                Identificador = "3",
                Modelo = "Susuki",
                Placa = "GBA-1G97"
            };
            var response = await Post<MotorcycleRequest>(motorcycleRequest, "motos");
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
        }
        [Fact]
        async Task UpdateLicensePlate_ValidRequest_ReturnsSuccess()
        {
            var motorcycleRequest = new MotorcycleLicensePlate()
            {
                Placa = "GBA-1G99"
            };
            var response = await Put<MotorcycleLicensePlate>(motorcycleRequest, "motos/1/placa");
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }
        [Fact]
        async Task UpdateLicensePlate_AlreadyExistsLicensePlateRequest_ReturnsBadRequest()
        {
            var motorcycleRequest = new MotorcycleLicensePlate()
            {
                Placa = "GBA-1G97"
            };

            var response = await Put<MotorcycleLicensePlate>(motorcycleRequest, "motos/1/placa");
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
        }
        [Fact]
        async Task DeleteMotorcycle_ExistsMotorcycle_ReturnsSuccess()
        {

            var response = await _httpClient.DeleteAsync("motos/1");
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }
        [Fact]
        async Task DeleteMotorcycle_NotExistsMotorcycle_ReturnsBadRequest()
        {

            var response = await _httpClient.DeleteAsync("motos/10");
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
        }
    }
}
