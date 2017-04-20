using System.Collections.Generic;
using System.Messaging;

namespace ConsoleApp.Utilities
{
    public class MqHandler
    {
        private readonly string path;

        public MqHandler(string path)
        {
            this.path = path;
        }

        public void Send<T>(T obj)
        {
            IMessageFormatter msgFormatter = new XmlMessageFormatter(new[] { typeof(T) });
            var message = new Message(obj, msgFormatter);

            var queue = new MessageQueue(path);
            queue.Send(message);
        }

        public IList<T> Receive<T>()
        {
            IMessageFormatter msgFormatter = new XmlMessageFormatter(new[] { typeof(T) });
            var queue = new MessageQueue(path);
            var messages = queue.GetAllMessages();

            IList<T> list = new List<T>();
            foreach (var message in messages)
            {
                if (null != message)
                {
                    message.Formatter = msgFormatter;
                    if (message.Body is T)
                        list.Add((T)message.Body);
                }

                queue.Receive();
            }

            return list;
        }
    }
}
