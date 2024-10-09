using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using RideWise.Api.Application.Models;
using RideWise.Api.Infrastructure;
using RideWise.IntegrationTest.Configurations;
using System.Net.Http.Headers;
using System.Text;

namespace RideWise.IntegrationTest
{
    public class BaseControllerIt 
    {
        protected readonly CustomWebApplicationFactory<Program> _factory;
        protected HttpClient _httpClient;
        public BaseControllerIt(CustomWebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _httpClient = _factory.CreateClient(new WebApplicationFactoryClientOptions()
            {
                AllowAutoRedirect = false
            });
            InitializeDbTest(feedDb: true);
        }
        public void InitializeDbTest(bool feedDb)
        {
            using (var scope = _factory.Services.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<RideWiseApiDbContext>();

                db.Database.EnsureCreated();
                db.Database.Migrate();
                if (feedDb)
                {
                    Seeding.FeedDataTestDB(db);
                }                
            }
        }
        public async Task<HttpResponseMessage> Post<T>(T request, string url)
        {
            var content = JsonConvert.SerializeObject(request);
            HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return await _httpClient.PostAsync(url, httpContent);
        }
        public async Task<HttpResponseMessage> Put<T>(T request, string url)
        {
            var content = JsonConvert.SerializeObject(request);
            HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return await _httpClient.PutAsync(url, httpContent);
        }
    }
}
