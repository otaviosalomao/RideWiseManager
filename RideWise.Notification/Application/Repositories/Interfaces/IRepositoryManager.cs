namespace RideWise.Notification.Application.Repositories.Interfaces
{
    public interface IRepositoryManager
    {
        IMotorcycleNoticeRepository MotorcycleNotice { get; }
        void Save();
    }
}
