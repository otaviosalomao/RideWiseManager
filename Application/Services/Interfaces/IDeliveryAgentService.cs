using ride_wise_api.Application.Models;

namespace ride_wise_api.Application.Services.Interfaces
{
    public interface IDeliveryAgentService
    {
        Task<DeliveryAgentResult> CreateAsync(DeliveryAgentRequest request);
        Task UpdateIdentificationDocumentImageAsync(string identification, string identificationDocumentImage);
    }
}
