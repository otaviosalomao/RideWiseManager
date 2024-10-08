using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace RideWise.Api.Application.Services.Interfaces
{
    public interface IMessageBusService
    {
        Task Publish(string message, string queue, string exchange);
        Task<IModel> Consume(string queue, string exchange);
    }
}
