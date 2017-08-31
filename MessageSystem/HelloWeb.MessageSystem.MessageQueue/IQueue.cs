namespace HelloWeb.MessageSystem.MessageQueue
{
    public interface IQueue
    {
        string Send(string queueName, string message);

        string Receive(string queueName);
    }
}
