using System;
using System.Collections.Generic;

namespace ConsoleApp
{
    public class WebHostBuilder : IWebHostBuilder
    {
        private IServer server;
        private readonly IList<Action<IApplicationBuilder>> configures = new List<Action<IApplicationBuilder>>();

        public IWebHost Build()
        {
            var builder = new ApplicationBuilder();
            foreach (var item in configures)
            {
                item(builder);
            }

            return new WebHost(server, builder.Build());
        }

        public IWebHostBuilder Configure(Action<IApplicationBuilder> configure)
        {
            configures.Add(configure);

            return this;
        }

        public IWebHostBuilder UseServer(IServer server)
        {
            this.server = server;

            return this;
        }
    }
}
