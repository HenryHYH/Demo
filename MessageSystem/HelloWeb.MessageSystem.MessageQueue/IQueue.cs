using System.Collections.Generic;

namespace HelloWeb.MessageSystem.MessageQueue
{
    public interface IQueue<T>
    {
        bool Send(T message, uint? delaySeconds = null, uint? priority = null);

        T Receive(uint? waitSeconds = null, bool deleteMessageAfterReceive = true);

        T Peek();

        int BatchSend(ICollection<T> messages, uint? delaySeconds = null, uint? priority = null);

        ICollection<T> BatchReceive(uint batchSize, uint? waitSeconds = null, bool deleteMessageAfterReceive = true);
    }
}
