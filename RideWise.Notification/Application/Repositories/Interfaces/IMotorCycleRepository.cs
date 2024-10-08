using RideWise.Notification.Domain.Models;

namespace RideWise.Notification.Application.Repositories.Interfaces
{
    public interface IMotorcycleNoticeRepository
    {
        Task<MotorcycleNotice> Create(MotorcycleNotice motorcycleNotice);
    }
}
