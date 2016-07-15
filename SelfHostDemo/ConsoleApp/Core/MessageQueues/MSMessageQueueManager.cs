using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Core.MessageQueues
{
    public class MSMessageQueueManager : IMessageQueueManager
    {
        #region Fields

        private readonly MessageQueue mq;

        #endregion

        #region Ctor

        public MSMessageQueueManager()
        {
            mq = new MessageQueue(".\\private$\\abc");
        }

        #endregion

        #region Methods

        public void Send<T>(T item)
        {
            mq.Send(item);
        }

        public void Receive<T>(Action<T> callback)
        {
            mq.Formatter = new XmlMessageFormatter(new Type[] { typeof(T) });
            mq.ReceiveCompleted += (sender, e) =>
            {
                using (var msg = mq.EndReceive(e.AsyncResult))
                {
                    var message = (T)msg.Body;
                    callback(message);
                }
                mq.BeginReceive();
            };
            mq.BeginReceive();
        }

        #endregion
    }
}
