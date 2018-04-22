using Newtonsoft.Json;
using RestSharp;
using System;

namespace ConsoleApp
{
    public static class RestSharpClient
    {
        private const string BASE_URL = "http://v.juhe.cn";
        private const string RESOURCE = "trademark/detail";
        private const string KEY = "6d16c24c29b5be631316a2849494992f";

        public static T Send<T>(string registerNo, string classification)
            where T : new()
        {
            var client = new RestClient(BASE_URL);

            var request = new RestRequest(RESOURCE, Method.POST);
            request.AddQueryParameter("key", KEY);
            request.AddQueryParameter("regNo", registerNo);
            request.AddQueryParameter("intCls", classification);

            var response = client.Execute<T>(request);
            var data = response.Data;
            if (null == data && !string.IsNullOrWhiteSpace(response.Content))
                data = JsonConvert.DeserializeObject<T>(response.Content);

            Console.WriteLine("Content = " + response.Content);

            return data;
        }
    }
}
