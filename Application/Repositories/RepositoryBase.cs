using ride_wise_api.Application.Repositories.Interfaces;
using ride_wise_api.Infrastructure;
using System.Linq.Expressions;
using System.Reflection;

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

        public IQueryable<T> GetAll()
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

        protected Expression GenerateDinamicExpression<F>(F filters, ParameterExpression parameter)
        {
            Expression? comparison = null;
            foreach (PropertyInfo propertyInfo in filters.GetType().GetProperties())
            {
                var propertyName = propertyInfo.Name;
                var value = propertyInfo.GetValue(filters, null);
                if (value is not null)
                {
                    var nameProperty = Expression.Property(parameter, propertyInfo.Name);
                    var nameConstant = Expression.Constant(propertyInfo.GetValue(filters, null));
                    if (comparison != null)
                    {
                        comparison = Expression.AndAlso(comparison, Expression.Equal(nameProperty, nameConstant));
                    }
                    if (comparison == null)
                    {
                        comparison = Expression.Equal(nameProperty, nameConstant);
                    }
                }
            }
            return comparison;
        }
    }
}
