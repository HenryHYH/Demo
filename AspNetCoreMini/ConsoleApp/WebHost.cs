using System.Threading.Tasks;

namespace ConsoleApp
{
    public class WebHost : IWebHost
    {
        private readonly IServer server;
        private readonly RequestDelegate handler;

        public WebHost(IServer server, RequestDelegate handler)
        {
            this.server = server;
            this.handler = handler;
        }

        public Task StartAsync()
        {
            return server.StartAsync(handler);
        }
    }
}
