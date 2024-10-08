using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;
using RideWise.Api.Application.Models;
using RideWise.Api.Application.Services.Interfaces;
using RideWise.Api.Infrastructure;
using System.Net.Mime;
using System.Text.Json;
using System.Text.Json.Serialization;
using Xunit;

namespace RideWise.IntegrationTest
{
    public class DeliveryAgentControllerIt : WebApplicationFactory<Program>
    {
        private const string BaseUri = "entregadores";
        private WebApplicationFactory<Program> _factory;
        private readonly Mock<IDeliveryAgentService> _deliveryAgentServiceMock;

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(services =>
            {
                services.RemoveAll(typeof(DbContextOptions<RideWiseApiDbContext>));
            });
        }
        public DeliveryAgentControllerIt(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _deliveryAgentServiceMock = new Mock<IDeliveryAgentService>();
        }

        [Fact]
        public async Task CreateDeliveryAgent_Responds200()
        {
            var client = _factory.CreateClient();
            var body = new DeliveryAgentRequest()
            {
                Data_nascimento = DateTime.Now,
                Numero_cnh = 123456,
                Tipo_cnh = "A",
                Identificador = "123",
                Cnpj = "123456789",
                Image_cnh = "as321das132d132asd132as312das312da132asd312"
            };
            var deliveryAgentResult = new DeliveryAgentResult()
            {
                Data_nascimento = DateTime.Now,
                Numero_cnh = 123456,
                Tipo_cnh = "A",
                Identificador = "123",
                Cnpj = "123456789"
            };
            _ = _deliveryAgentServiceMock.Setup(_ => _.CreateAsync(body)).Returns(Task.FromResult(deliveryAgentResult));
            var httpClient = _factory.WithWebHostBuilder(builder => builder.ConfigureServices(services => services.AddScoped(_ => _deliveryAgentServiceMock.Object)))
                .CreateClient();
            var response = await httpClient.PostAsync(BaseUri, toHttpContent(body));
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            AssertHasAllDeliveryAgentResultField(responseBody);
        }

        private static HttpContent toHttpContent<T>(T body)
        {
            var appCustomOptions = new JsonSerializerOptions();
            appCustomOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase, allowIntegerValues: false));
            var bodyAsJsonStr = JsonSerializer.Serialize(body, appCustomOptions);
            var bodyAsJsonBuffer = System.Text.Encoding.UTF8.GetBytes(bodyAsJsonStr);
            var httpContent = new ByteArrayContent(bodyAsJsonBuffer);
            httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(MediaTypeNames.Application.Json);
            return httpContent;
        }
        private static void AssertHasAllDeliveryAgentResultField(string responseBody)
        {
            Assert.Contains("Identificador", responseBody);
        }
    }
}
