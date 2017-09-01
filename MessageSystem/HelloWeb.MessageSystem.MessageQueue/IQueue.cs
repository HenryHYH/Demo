namespace HelloWeb.MessageSystem.MessageQueue
{
    public interface IQueue<T>
    {
        string Send(T message, uint? delaySeconds = null, uint? priority = null);

        T Receive(uint? waitSeconds = null, bool deleteMessageAfterReceive = true);

        T Peek();
    }
}
