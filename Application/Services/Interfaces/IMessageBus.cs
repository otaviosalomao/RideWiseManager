namespace ride_wise_api.Application.Services.Interfaces
{
    public interface IMessageBusService
    {
        Task Publish(string message, string queue, string exchange);
    }
}
