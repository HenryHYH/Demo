using System.Threading.Tasks;

namespace ConsoleApp
{
    public class Program
    {
        public static void Main()
        {
            MainImpl().GetAwaiter().GetResult();
        }

        public static async Task MainImpl()
        {
            await new WebHostBuilder()
                .UseHttpListener()
                .Configure(app => app
                    .Use(FooMiddleware)
                    .Use(BarMiddleware)
                    .Use(BazMiddleware))
                .Build()
                .StartAsync();
        }

        private static RequestDelegate FooMiddleware(RequestDelegate next)
        {
            return async context =>
            {
                await context.Response.WriteAsync("Foo=>");
                await next(context);
            };
        }

        private static RequestDelegate BarMiddleware(RequestDelegate next)
        {
            return async context =>
            {
                await context.Response.WriteAsync("Bar=>");
                await next(context);
            };
        }

        private static RequestDelegate BazMiddleware(RequestDelegate next)
        {
            return async context => await context.Response.WriteAsync("Baz");
        }
    }
}
