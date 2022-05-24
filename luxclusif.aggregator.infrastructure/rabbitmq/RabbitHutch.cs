using luxclusif.aggregator.application.Interfaces;
using RabbitMQ.Client;

namespace luxclusif.aggregator.infrastructure.rabbitmq
{
    public class RabbitHutch
    {
        private static ConnectionFactory _factory;
        private static IConnection _connection;
        private static IModel _channel;

        
        public static IMessageReceiverInterface CreateBus(
            string? hostName,
            string? hostPort,
            string? virtualHost,
            string? username,
            string? password)
        {
            _factory = new ConnectionFactory();

            if(!string.IsNullOrEmpty(hostName))
                _factory.HostName = hostName;

            if (!string.IsNullOrEmpty(hostPort))
            {
                var port = int.Parse(hostPort);
                _factory.Port = port;
            }

            if (!string.IsNullOrEmpty(virtualHost))
                _factory.VirtualHost = virtualHost;

            if (!string.IsNullOrEmpty(username))
                _factory.UserName = username;

            if (!string.IsNullOrEmpty(password))
                _factory.Password = password;

            _factory.DispatchConsumersAsync = true;

            _connection = _factory.CreateConnection();
            _channel = _connection.CreateModel();

            return new ReceiveMessageRabbitmq(_channel);
        }

    }
}
