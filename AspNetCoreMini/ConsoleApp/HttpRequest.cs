using System;
using System.Collections.Specialized;
using System.IO;

namespace ConsoleApp
{
    public class HttpRequest
    {
        private readonly IHttpRequestFeature feature;

        public HttpRequest(IFeatureCollection features)
        {
            feature = features.Get<IHttpRequestFeature>();
        }

        public Uri Uri => feature.Uri;

        public NameValueCollection Header => feature.Headers;

        public Stream Body => feature.Body;
    }
}