using ride_wise_api.Application.Models;
using ride_wise_api.Domain.Models;

namespace ride_wise_api.Application.Repositories.Interfaces
{
    public interface IRentalRepository
    {
        Task<Rental> Create(Rental Rental);
        Task<Rental> Get(RentalFilter filters);
        Task Update(Rental Rental);
    }
}
