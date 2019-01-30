using System;

namespace ConsoleApp
{
    public interface IWebHostBuilder
    {
        IWebHostBuilder UseServer(IServer server);

        IWebHostBuilder Configure(Action<IApplicationBuilder> configure);

        IWebHost Build();
    }
}
