using ride_wise_api.Application.Models;
using ride_wise_api.Application.Repositories.Interfaces;
using ride_wise_api.Domain.Models;
using ride_wise_api.Infrastructure;
using System.Linq.Expressions;

namespace ride_wise_api.Application.Repositories
{
    public class MotorcycleRepository : RepositoryBase<Motorcycle>, IMotorcycleRepository
    {
        public MotorcycleRepository(RiseWiseManagerDbContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task<Motorcycle> Create(Motorcycle motorcycle)
        {
            return base.Create(motorcycle);
        }

        public async Task Delete(Motorcycle motorcycle)
        {
            base.Delete(motorcycle);
        }

        public async Task<IEnumerable<Motorcycle>> Get(MotorcycleFilter filters)
        {
            var parameter = Expression.Parameter(typeof(Motorcycle), "x");
            Expression? comparison = GenerateDinamicExpression(filters, parameter);
            if (comparison is not null)
            {
                var lambda = Expression.Lambda<Func<Motorcycle, bool>>(comparison, parameter);
                return FindByCondition(lambda);
            }
            return GetAll();
        }

        public async Task Update(Motorcycle motorcycle)
        {
            base.Update(motorcycle);
        }
    }
}
