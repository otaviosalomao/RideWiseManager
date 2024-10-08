using RideWise.Api.Domain.Models;

namespace RideWise.Api.Application.Services.Interfaces
{
    public interface IMotorcycleMessageBusProducerService
    {
        public Task Publish(Motorcycle motorcycle);
    }
}
