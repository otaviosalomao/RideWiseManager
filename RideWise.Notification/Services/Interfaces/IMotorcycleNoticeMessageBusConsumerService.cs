namespace RideWise.RabbitMqConsumer.Services.Interfaces
{
    public interface IMotorcycleNoticeMessageBusConsumerService
    {
        Task ProcessAsync();
    }
}
