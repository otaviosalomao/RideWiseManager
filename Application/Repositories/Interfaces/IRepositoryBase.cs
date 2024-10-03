using System.Linq.Expressions;

namespace ride_wise_api.Application.Repositories.Interfaces
{
    public interface IRepositoryBase<T>
    {
        IQueryable<T> FindAll();
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> exmpression);
        T Create(T entity);
        void Delete(T entity);
        void Update(T entity);
    }
}
