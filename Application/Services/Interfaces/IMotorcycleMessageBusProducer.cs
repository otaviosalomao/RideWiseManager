using ride_wise_api.Domain.Models;

namespace ride_wise_api.Application.Services.Interfaces
{
    public interface IMotorcycleMessageBusProducer
    {
        public Task Publish(Motorcycle motorcycle);
    }
}
