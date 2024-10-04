namespace ride_wise_api.Application.Services.Interfaces
{
    public interface IRabbitMqService
    {
        Task Publish(byte[] body, string queue, string exchange);
        Task<byte[]> Consume(string queue, string exchange);
    }
}
