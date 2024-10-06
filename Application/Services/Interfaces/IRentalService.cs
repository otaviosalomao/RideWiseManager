using ride_wise_api.Application.Models;

namespace ride_wise_api.Application.Services.Interfaces
{
    public interface IRentalService
    {
        Task<RentalResult> CreateAsync(RentalRequest request);
        Task<RentalResult> GetAsync(RentalFilter filters);
        Task<bool> UpdateDevolutionDateAsync(string identification, DateTime devolutionDate);
    }
}
