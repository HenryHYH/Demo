using HelloWeb.MessageSystem.OpenSearch.Models;
using System;
using System.Collections.Generic;

namespace HelloWeb.MessageSystem.OpenSearch
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start");

            IOpenSearchAPI client = new OpenSearchAPI(new Uri("http://localhost:8080/"), new AnonymousCredential());
            // TestSearch(client);
            // TestSuggest(client);
            TestAddDocument(client);

            Console.WriteLine("Finish");
            Console.ReadKey();
        }

        private static void TestAddDocument(IOpenSearchAPI client)
        {
            var request = new DocumentRequest();
            request.AppName = "helloworld";
            request.TableName = "product";
            IDictionary<string, object> fields = new Dictionary<string, object>();
            fields.Add("id", 8);
            fields.Add("title", "追风筝的人");
            fields.Add("description", @"12岁的阿富汗富家少爷阿米尔与仆人哈桑情同手足。然而，在一场风筝比赛后，发生了一件悲惨不堪的事，阿米尔为自己的懦弱感到自责和痛苦，逼走了哈桑，不久，自己也跟随父亲逃往美国。
成年后的阿米尔始终无法原谅自己当年对哈桑的背叛。为了赎罪，阿米尔再度踏上暌违二十多年的故乡，希望能为不幸的好友尽最后一点心力，却发现一个惊天谎言，儿时的噩梦再度重演，阿米尔该如何抉择？
故事如此残忍而又美丽，作者以温暖细腻的笔法勾勒人性的本质与救赎，读来令人荡气回肠。");
            fields.Add("author_id", 1);
            request.Fields = fields;

            var response = client.AddDocumentUsingPOST(request);
            if (response.Success.HasValue && response.Success.Value)
                Console.WriteLine(response.Json);
            else
                Console.WriteLine(response.Error);
        }

        private static void TestSearch(IOpenSearchAPI client)
        {
            var request = new SearchRequest();
            request.AppName = "helloworld";
            request.Hits = 5;
            request.Query = "default:'搜索引擎'";

            var response = client.SearchUsingPOST(request);
            if (response.Success.HasValue && response.Success.Value)
                Console.WriteLine(response.Json);
            else
                Console.WriteLine(response.Error);
        }

        private static void TestSuggest(IOpenSearchAPI client)
        {
            var request = new SuggestRequest();
            request.AppName = "helloworld";
            request.Hits = 5;
            request.SuggestName = "hello_dropdown";
            request.Query = "sousuo";

            var response = client.SuggestUsingPOST(request);
            if (response.Success.HasValue && response.Success.Value)
                Console.WriteLine(response.Json);
            else
                Console.WriteLine(response.Error);
        }
    }
}
