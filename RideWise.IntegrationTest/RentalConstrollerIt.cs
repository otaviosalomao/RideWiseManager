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
    public class RentalControllerIt : BaseControllerIt, IClassFixture<CustomWebApplicationFactory<Program>>
    {
        public RentalControllerIt(CustomWebApplicationFactory<Program> factory) : base(factory)
        {
            
        }

        [Fact]
        async Task CreateRental_ValidRental_ReturnsSuccess()
        {            
            var rentalRequest = new RentalRequest()
            {
                Entregador_id = "4",
                Moto_id = "4",
                Data_inicio = DateTime.Now,
                Data_termino = DateTime.Now.AddDays(7),
                Data_previsao_termino = DateTime.Now.AddDays(7),
                Plano = 7
            };

            var response = await Post<RentalRequest>(rentalRequest, "locacao");
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);
        }
        [Fact]
        async Task CreateRental_AlreadyExistsRental_ReturnsBadRequest()
        {            
            var rentalRequest = new RentalRequest()
            {
                Entregador_id = "1",
                Moto_id = "1",
                Data_inicio = DateTime.Now,
                Data_termino = DateTime.Now.AddDays(7),
                Data_previsao_termino = DateTime.Now.AddDays(7),
                Plano = 7
            };

            var response = await Post<RentalRequest>(rentalRequest, "locacao");
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
        }
        [Fact]
        async Task CreateRental_InvalidEstimatedEndDate_ReturnsBadRequest()
        {            
            var rentalRequest = new RentalRequest()
            {
                Entregador_id = "1",
                Moto_id = "1",
                Data_inicio = DateTime.Now,
                Data_termino = DateTime.Now.AddDays(7),
                Data_previsao_termino = DateTime.Now.AddDays(6),
                Plano = 7
            };            
            var response = await Post<RentalRequest>(rentalRequest, "locacao");
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
        }
        [Fact]
        async Task CreateRental_InvalidEndDate_ReturnsBadRequest()
        {            
            var rentalRequest = new RentalRequest()
            {
                Entregador_id = "1",
                Moto_id = "1",
                Data_inicio = DateTime.Now,
                Data_termino = DateTime.Now.AddDays(6),
                Data_previsao_termino = DateTime.Now.AddDays(7),
                Plano = 7
            };

            var response = await Post<RentalRequest>(rentalRequest, "locacao");
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
        }
        [Fact]
        async Task CreateRental_InvalidPlanNumber_ReturnsBadRequest()
        {            
            var rentalRequest = new RentalRequest()
            {
                Entregador_id = "1",
                Moto_id = "1",
                Data_inicio = DateTime.Now,
                Data_termino = DateTime.Now.AddDays(7),
                Data_previsao_termino = DateTime.Now.AddDays(7),
                Plano = 6
            };

            var response = await Post<RentalRequest>(rentalRequest, "locacao");
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
        }
        [Fact]
        async Task UpdateDevolutionDate_ValidId_ReturnsSuccess()
        {            
            var rentalDevolutionDate = new RentalDevolutionDate()
            {
                Data_devolucao = DateTime.Now.AddDays(10)
            };            
            var response = await Put<RentalDevolutionDate>(rentalDevolutionDate, "locacao/1/devolucao");
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }
        [Fact]
        async Task UpdateDevolutionDate_invalidId_ReturnsBadRequest()
        {            
            var rentalDevolutionDate = new RentalDevolutionDate()
            {
                Data_devolucao = DateTime.Now.AddDays(10)
            };            
            var response = await Put<RentalDevolutionDate>(rentalDevolutionDate, "locacao/10/devolucao");
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
        }
    }
}
