using RideWise.Api.Application.Models;
using RideWise.Api.Domain.Models;

namespace RideWise.Api.Application.Repositories.Interfaces
{
    public interface IRentalRepository
    {
        Task<Rental> Create(Rental Rental);
        Task<Rental> Get(RentalFilter filters);
        Task Update(Rental Rental);
    }
}
