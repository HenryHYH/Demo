using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace WebApiSdk.Common
{
    public class ApiClient
    {
        public string BaseAddress { get; set; }

        public string MediaType { get; set; }

        protected virtual HttpClient GetClient()
        {
            var handler = new HttpClientHandler()
            {
                AllowAutoRedirect = false,
                UseProxy = false
            };

            var client = new HttpClient(handler);
            client.BaseAddress = new Uri(BaseAddress);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaType));

            return client;
        }

        public virtual async Task<T> GetAsync<T>(string path)
        {
            var result = default(T);

            var client = GetClient();
            var response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadAsAsync<T>();
            }

            return result;
        }
    }
}
