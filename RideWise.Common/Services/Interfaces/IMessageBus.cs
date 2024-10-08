namespace RideWise.Api.Application.Services.Interfaces
{
    public interface IMessageBusService
    {
        Task Publish(string message, string queue, string exchange);
        Task Consume(string queue, string exchange);
    }
}
