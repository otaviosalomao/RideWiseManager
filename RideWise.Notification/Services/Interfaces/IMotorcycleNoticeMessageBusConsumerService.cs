using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RideWise.RabbitMqConsumer.Services.Interfaces
{
    public interface IMotorcycleNoticeMessageBusConsumerService
    {
        Task ProcessAsync();
    }
}
