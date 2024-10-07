using RideWise.Api.Application.Models;
using RideWise.Api.Application.Repositories.Interfaces;
using RideWise.Api.Domain.Models;
using RideWise.Api.Infrastructure;
using System.Linq.Expressions;

namespace RideWise.Api.Application.Repositories
{
    public class DeliveryAgentRepository : RepositoryBase<DeliveryAgent>, IDeliveryAgentRepository
    {
        public DeliveryAgentRepository(RiseWiseManagerDbContext repositoryContext) : base(repositoryContext)
        {
        }
        public async Task<DeliveryAgent> Create(DeliveryAgent deliveryAgent)
        {
            return base.Create(deliveryAgent);
        }
        public async Task<bool> Exists(string Identification)
        {
            var filter = new DeliveryAgentFilter(
               identification: Identification);
            var result = await Get(filter);
            return result is not null;
        }
        public async Task<DeliveryAgent> Get(DeliveryAgentFilter filters)
        {
            var parameter = Expression.Parameter(typeof(DeliveryAgent), "x");
            Expression? comparison = GenerateDinamicExpression(filters, parameter);
            if (comparison is not null)
            {
                var lambda = Expression.Lambda<Func<DeliveryAgent, bool>>(comparison, parameter);
                return FindByCondition(lambda).FirstOrDefault();
            }
            return GetAll().FirstOrDefault();
        }      
        public async Task Update(DeliveryAgent deliveryAgent)
        {
            base.Update(deliveryAgent);
        }
    }
}
