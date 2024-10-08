using System.Linq.Expressions;

namespace RideWise.Notification.Application.Repositories.Interfaces
{
    public interface IRepositoryBase<T>
    {   
        T Create(T entity);       
    }
}
