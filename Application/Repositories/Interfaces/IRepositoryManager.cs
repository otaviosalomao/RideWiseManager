namespace ride_wise_api.Application.Repositories.Interfaces
{
    public interface IRepositoryManager
    {
        IMotorcycleRepository Motorcycle { get; }
        IMotorcycleRepository DeliveryAgent { get; }
        void Save();
    }
}
