using System;
using System.Collections.Specialized;
using System.IO;
using System.Net;

namespace ConsoleApp
{
    public class HttpListenerFeature : IHttpRequestFeature, IHttpResponseFeature
    {
        private readonly HttpListenerContext context;

        public HttpListenerFeature(HttpListenerContext context)
        {
            this.context = context;
        }

        NameValueCollection IHttpRequestFeature.Headers => context.Request.Headers;

        NameValueCollection IHttpResponseFeature.Headers => context.Response.Headers;

        Stream IHttpRequestFeature.Body => context.Request.InputStream;

        Stream IHttpResponseFeature.Body => context.Response.OutputStream;

        int IHttpResponseFeature.StatusCode { get => context.Response.StatusCode; set => context.Response.StatusCode = value; }

        Uri IHttpRequestFeature.Uri => context.Request.Url;
    }
}
