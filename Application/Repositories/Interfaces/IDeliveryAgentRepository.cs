using ride_wise_api.Application.Models;
using ride_wise_api.Domain.Models;

namespace ride_wise_api.Application.Repositories.Interfaces
{
    public interface IDeliveryAgentRepository
    {
        Task<DeliveryAgent> Create(DeliveryAgent deliveryAgent);
        Task<DeliveryAgent> Get(DeliveryAgentFilter filters);
        Task Update(DeliveryAgent deliveryAgent);
        Task<bool> Exists(string Identification);
    }
}
