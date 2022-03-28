namespace luxclusif.aggregator.application.Interfaces
{
    public interface IMessageReceiverInterface
    {
        Task ReceiveAsync<T>(string queue, Action<T> onMessage);
    }
}
