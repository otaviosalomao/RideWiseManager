namespace ride_wise_api.Application.Repositories.Interfaces
{
    public interface IRepositoryManager
    {
        IMotorCycleRepository MotorCycle {  get; }
        void Save();
    }
}
