using Microsoft.OpenApi.Models;
using RideWise.Api.Extensions;
using RideWise.Api.Infrastructure;
using RideWise.Common.Extensions;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.ConfigureServices();
builder.Services.ConfigureCommonServices();
builder.Services.ConfigureDbContext(builder.Configuration);
builder.Services.ConfigureRepositories();
builder.Services.ConfigureLogger();
builder.Services.ConfigureMapper();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "RideWise.API",
        Version = "1.0.0",
        Contact = new OpenApiContact
        {
            Name = "Otavio Martins Salomao",
            Email = "otaviosalomao@gmail.com",
            Url = new Uri("https://www.linkedin.com/in/otaviosalomao/")
        }
    });
    var xmlFile = "RideWise.API.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});
builder.Services.AddValidators();
builder.Services.ConfigureRabbitMQ(builder.Configuration);

var app = builder.Build();
MigrationService.InitializaMigration(app);
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();
app.Run();

public partial class Program { }