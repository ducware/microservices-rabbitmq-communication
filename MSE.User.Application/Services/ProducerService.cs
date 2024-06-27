using MassTransit;
using MSE.Common.Configurations;

namespace MSE.User.Application.Services
{
    public class ProducerService : IProducerService
    {
            private IBus _bus;
            private readonly RabbitMqConfigs _rabbitMqConfigs;

            public ProducerService(IBus bus, RabbitMqConfigs rabbitMqConfigs)
            {
                _bus = bus;
                _rabbitMqConfigs = rabbitMqConfigs;
            }

            public async Task SendAsync(string queueName, object data)
            {
                if (data != null)
                {
                    Uri uri = new Uri($"{_rabbitMqConfigs.Host}/{queueName}");
                    var endPoint = await _bus.GetSendEndpoint(uri);
                    await endPoint.Send(data);
                }
            }
    }
}
