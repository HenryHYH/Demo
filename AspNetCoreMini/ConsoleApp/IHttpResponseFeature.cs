using System.Collections.Specialized;
using System.IO;

namespace ConsoleApp
{
    public interface IHttpResponseFeature
    {
        NameValueCollection Headers { get; }

        Stream Body { get; }

        int StatusCode { get; set; }
    }
}
