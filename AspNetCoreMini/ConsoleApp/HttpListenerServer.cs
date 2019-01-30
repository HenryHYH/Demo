using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class HttpListenerServer : IServer
    {
        private readonly HttpListener httpListener;
        private readonly string[] urls;

        public HttpListenerServer(params string[] urls)
        {
            httpListener = new HttpListener();
            this.urls = urls.Any() ? urls : new[] { "http://localhost:5000/" };
        }

        public async Task StartAsync(RequestDelegate handler)
        {
            Array.ForEach(urls, url => httpListener.Prefixes.Add(url));
            httpListener.Start();
            while (true)
            {
                var listenerContext = await httpListener.GetContextAsync();
                var feature = new HttpListenerFeature(listenerContext);
                var featrues = new FeatureCollection()
                                    .Set<IHttpRequestFeature>(feature)
                                    .Set<IHttpResponseFeature>(feature);
                var httpContext = new HttpContext(featrues);
                await handler(httpContext);
                listenerContext.Response.Close();
            }
        }
    }
}
