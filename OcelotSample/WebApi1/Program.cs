using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace WebApi1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                .UseUrls("http://*:5001")
                .UseStartup<Startup>();
        }
    }
}
