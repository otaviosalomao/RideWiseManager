using RideWise.Api.Application.Models;

namespace RideWise.Api.Application.Services.Interfaces
{
    public interface IDeliveryAgentService
    {
        Task<DeliveryAgentResult> CreateAsync(DeliveryAgentRequest request);
        Task UpdateDriverLicenseImageAsync(string identification, string identificationDocumentImage);
    }
}
