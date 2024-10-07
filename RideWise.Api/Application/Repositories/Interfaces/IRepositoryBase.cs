using System.Linq.Expressions;

namespace RideWise.Api.Application.Repositories.Interfaces
{
    public interface IRepositoryBase<T>
    {
        IQueryable<T> GetAll();
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);
        T Create(T entity);
        void Delete(T entity);
        void Update(T entity);
    }
}
