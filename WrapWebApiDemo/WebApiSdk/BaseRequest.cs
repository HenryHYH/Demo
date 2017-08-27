using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiSdk.Common;

namespace WebApiSdk
{
    public class BaseRequest<TResponse>
    {
        private ApiClient client;
        public string Uri { get; set; }

        private BaseRequest()
        {
            client = new ApiClient
            {
                BaseAddress = "http://localhost:4598/",
                MediaType = "application/json"
            };
        }

        public BaseRequest(string uri)
            : this()
        {
            Uri = uri;
        }

        public async Task<ApiResponse<TResponse>> Execute()
        {
            var response = new ApiResponse<TResponse>();

            try
            {
                var result = await client.GetAsync<TResponse>(Uri);
                response.IsSuccess = true;
                response.Result = result;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Errors.Add(ex.Message);
            }

            return response;
        }
    }
}
