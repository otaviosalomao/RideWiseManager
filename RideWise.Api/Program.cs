using RideWise.Api.Application.Services;
using RideWise.Api.Extensions;
using RideWise.Common.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.ConfigureServices();
builder.Services.ConfigureCommonServices();
builder.Services.ConfigureDbContext(builder.Configuration);
builder.Services.ConfigureRepositories();
builder.Services.ConfigureLogger();
builder.Services.ConfigureMapper();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
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
