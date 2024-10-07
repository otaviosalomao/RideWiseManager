using RideWise.Api.Application.Models;

namespace RideWise.Api.Application.Services.Interfaces
{
    public interface IRentalService
    {
        Task<RentalResult> CreateAsync(RentalRequest request);
        Task<RentalResult> GetAsync(RentalFilter filters);
        Task<bool> UpdateDevolutionDateAsync(string identification, DateTime devolutionDate);
    }
}
