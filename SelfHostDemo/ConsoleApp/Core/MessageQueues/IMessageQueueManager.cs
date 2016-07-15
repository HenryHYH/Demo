using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Core.MessageQueues
{
    public interface IMessageQueueManager
    {
        void Send<T>(T item);

        void Receive<T>(Action<T> callback);
    }
}
