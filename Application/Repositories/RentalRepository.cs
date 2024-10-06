using ride_wise_api.Application.Models;
using ride_wise_api.Application.Repositories.Interfaces;
using ride_wise_api.Domain.Models;
using ride_wise_api.Infrastructure;
using System.Linq.Expressions;

namespace ride_wise_api.Application.Repositories
{
    public class RentalRepository : RepositoryBase<Rental>, IRentalRepository
    {
        public RentalRepository(RiseWiseManagerDbContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task<Rental> Create(Rental Rental)
        {
            return base.Create(Rental);
        }       

        public async Task<Rental> Get(RentalFilter filters)
        {
            var parameter = Expression.Parameter(typeof(Rental), "x");
            Expression? comparison = GenerateDinamicExpression(filters, parameter);
            if (comparison is not null)
            {
                var lambda = Expression.Lambda<Func<Rental, bool>>(comparison, parameter);
                return FindByCondition(lambda).FirstOrDefault();
            }
            return GetAll().FirstOrDefault();
        }

        public async Task Update(Rental Rental)
        {
            base.Update(Rental);
        }
    }
}
