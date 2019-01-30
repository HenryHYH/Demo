using System;
using System.Collections.Specialized;
using System.IO;

namespace ConsoleApp
{
    public interface IHttpRequestFeature
    {
        Uri Uri { get; }

        NameValueCollection Headers { get; }

        Stream Body { get; }
    }
}
