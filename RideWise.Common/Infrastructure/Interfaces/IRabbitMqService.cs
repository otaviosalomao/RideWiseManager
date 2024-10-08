using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace RideWise.Api.Application.Services.Interfaces
{
    public interface IRabbitMqService
    {
        Task Publish(byte[] body, string queue, string exchange);
        Task<IModel> Consume(string queue, string exchange);
    }
}
