using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace RideWise.Common.Infrastructure
{
    public interface IRabbitMqService
    {
        Task Publish(byte[] body, string queue, string exchange);        
    }
}
