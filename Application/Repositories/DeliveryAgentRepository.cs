using ride_wise_api.Application.Models;
using ride_wise_api.Application.Repositories.Interfaces;
using ride_wise_api.Domain.Models;
using ride_wise_api.Infrastructure;
using System.Linq.Expressions;

namespace ride_wise_api.Application.Repositories
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
