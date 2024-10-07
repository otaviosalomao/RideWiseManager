using RideWise.Api.Application.Models;
using RideWise.Api.Application.Repositories.Interfaces;
using RideWise.Api.Domain.Models;
using RideWise.Api.Infrastructure;
using System.Linq.Expressions;

namespace RideWise.Api.Application.Repositories
{
    public class RentalRepository : RepositoryBase<Rental>, IRentalRepository
    {
        public RentalRepository(RideWiseApiDbContext repositoryContext) : base(repositoryContext)
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
