using RideWise.Notification.Application.Repositories.Interfaces;
using RideWise.Notification.Infrastructure;

namespace RideWise.Notification.Application.Repositories
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        private readonly RideWiseNotificationDbContext repositoryContext;

        public RepositoryBase(RideWiseNotificationDbContext repositoryContext)
        {
            this.repositoryContext = repositoryContext;
        }

        public T Create(T entity)
        {
            repositoryContext.Set<T>().Add(entity);
            return entity;
        }        
    }
}
