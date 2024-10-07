using RideWise.Api.Application.Models;
using RideWise.Api.Domain.Models;

namespace RideWise.Api.Application.Repositories.Interfaces
{
    public interface IDeliveryAgentRepository
    {
        Task<DeliveryAgent> Create(DeliveryAgent deliveryAgent);
        Task<DeliveryAgent> Get(DeliveryAgentFilter filters);
        Task Update(DeliveryAgent deliveryAgent);
        Task<bool> Exists(string Identification);
    }
}
