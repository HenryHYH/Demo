using System.Collections.Specialized;
using System.IO;

namespace ConsoleApp
{
    public class HttpResponse
    {
        private readonly IHttpResponseFeature feature;

        public HttpResponse(IFeatureCollection features)
        {
            feature = features.Get<IHttpResponseFeature>();
        }

        public NameValueCollection Header => feature.Headers;

        public Stream Body => feature.Body;

        public int StatusCode { get => feature.StatusCode; set => feature.StatusCode = value; }
    }
}