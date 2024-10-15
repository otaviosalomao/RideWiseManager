namespace RideWise.Common.Services.Interfaces
{
    public interface IMessageBusService
    {
        Task Publish(string message, string queue, string exchange);        
    }
}
