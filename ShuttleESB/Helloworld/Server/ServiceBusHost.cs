using System;
using Shuttle.Core.Host;
using Shuttle.ESB.Core;

namespace Server
{
    public class ServiceBusHost : IHost, IDisposable
    {
        private static IServiceBus bus;

        public void Dispose()
        {
            bus.Dispose();
        }

        public void Start()
        {
            bus = ServiceBus.Create().Start();
        }
    }
}
