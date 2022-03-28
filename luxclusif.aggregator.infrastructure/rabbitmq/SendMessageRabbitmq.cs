using luxclusif.aggregator.application.Interfaces;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace luxclusif.aggregator.infrastructure.rabbitmq
{
    public class ReceiveMessageRabbitmq : IMessageReceiverInterface
    {
        private readonly IModel _channel;

        public ReceiveMessageRabbitmq(IModel channel)
        {
            _channel = channel;
        }

        public async Task ReceiveAsync<T>(string queue, Action<T> onMessage)
        {
            _channel.QueueDeclare(queue, true, false, false);
            var consumer = new AsyncEventingBasicConsumer(_channel);
            consumer.Received += async (s, e) =>
            {
                var jsonSpecified = Encoding.UTF8.GetString(e.Body.Span);

                var item = JsonConvert.DeserializeObject<T>(jsonSpecified);

                onMessage(item!);
                await Task.Yield();
            };
            _channel.BasicConsume(queue, true, consumer);
            await Task.Yield();
        }
    }
}
