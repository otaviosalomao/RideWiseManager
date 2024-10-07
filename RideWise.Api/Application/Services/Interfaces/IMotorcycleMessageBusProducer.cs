using RideWise.Api.Domain.Models;

namespace RideWise.Api.Application.Services.Interfaces
{
    public interface IMotorcycleMessageBusProducer
    {
        public Task Publish(Motorcycle motorcycle);
    }
}
