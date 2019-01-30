namespace ConsoleApp
{
    public class HttpContext
    {
        public HttpContext(IFeatureCollection features)
        {
            Request = new HttpRequest(features);
            Response = new HttpResponse(features);
        }

        public HttpRequest Request { get; }

        public HttpResponse Response { get; }
    }
}
