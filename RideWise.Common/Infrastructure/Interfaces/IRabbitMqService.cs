namespace RideWise.Api.Application.Services.Interfaces
{
    public interface IRabbitMqService
    {
        Task Publish(byte[] body, string queue, string exchange);
        Task Consume(string queue, string exchange);
    }
}
