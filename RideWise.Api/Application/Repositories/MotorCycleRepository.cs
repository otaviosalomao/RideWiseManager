using RideWise.Api.Application.Models;
using RideWise.Api.Application.Repositories.Interfaces;
using RideWise.Api.Domain.Models;
using RideWise.Api.Infrastructure;
using System.Linq.Expressions;

namespace RideWise.Api.Application.Repositories
{
    public class MotorcycleRepository : RepositoryBase<Motorcycle>, IMotorcycleRepository
    {
        public MotorcycleRepository(RideWiseApiDbContext repositoryContext) : base(repositoryContext)
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

        public async Task<bool> Exists(string Identification)
        {
            var filter = new MotorcycleFilter(
               identification: Identification);
            var result = await Get(filter);
            return result.Any();
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
