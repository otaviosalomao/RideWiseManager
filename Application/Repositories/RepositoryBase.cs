using ride_wise_api.Application.Repositories.Interfaces;
using ride_wise_api.Infrastructure;
using System.Linq.Expressions;

namespace ride_wise_api.Application.Repositories
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        private readonly RiseWiseManagerDbContext repositoryContext;

        public RepositoryBase(RiseWiseManagerDbContext repositoryContext)
        {
            this.repositoryContext = repositoryContext;
        }

        public T Create(T entity)
        {
            repositoryContext.Set<T>().Add(entity);
            return entity;
        }

        public void Delete(T entity)
        {
            repositoryContext.Set<T>().Remove(entity);
        }

        public IQueryable<T> FindAll()
        {
            return repositoryContext.Set<T>();
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return repositoryContext.Set<T>().Where(expression);
        }

        public void Update(T entity)
        {
            repositoryContext.Set<T>().Update(entity);
        }
    }
}
