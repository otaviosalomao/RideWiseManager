﻿namespace RideWise.Api.Application.Repositories.Interfaces
{
    public interface IRepositoryManager
    {
        IMotorcycleRepository Motorcycle { get; }
        IDeliveryAgentRepository DeliveryAgent { get; }
        IRentalRepository Rental { get; }
        void Save();
    }
}
