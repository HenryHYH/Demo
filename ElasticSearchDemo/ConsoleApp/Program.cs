using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using ConsoleApp.Models;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var node = new Uri("http://localhost:9200");
            var settings = new ConnectionSettings(node, "user_packet")
                                .ExposeRawResponse(true)
                                .SetDefaultPropertyNameInferrer(x => x)
                                .SetDefaultTypeNameInferrer(x => x.Name);
            var client = new ElasticClient(settings);

            // TestAggr(client);
            // TestGroupBy(client);
            TestGroupByKey(client);

            Console.Write("Press any key to continue...");
            Console.ReadKey(true);
        }

        public static void TestGroupByKey(IElasticClient client)
        {
            var response = client.Search<UserBehavior>(x => x
                                    .Index("logstash-user-packet")
                                    .Size(0)
                                    .Aggregations(a => a
                                        .Terms("count", t => t
                                            .Field("EventKey.raw")
                                            .Aggregations(a2 => a2
                                                .Cardinality("cardinality", c => c
                                                    .Field("DeviceId.raw"))
                                                .ValueCount("count", vc => vc
                                                    .Field("DeviceId.raw"))
                                            )
                                        )
                                    ));
            if (null != response)
            {
                Console.WriteLine("Total = {0}", response.Total);
                var buckets = response.Aggs.Terms("count");
                foreach (var item in buckets.Items)
                {
                    Console.WriteLine("{0} = {1}, {2}, {3}",
                                        item.Key,
                                        item.DocCount,
                                        item.Cardinality("cardinality").Value,
                                        item.ValueCount("count").Value);
                }
            }
        }

        public static void TestGroupBy(IElasticClient client)
        {
            var response = client.Search<UserBehavior>(x => x
                                    .Index("logstash-user-packet")
                                    .Size(0)
                                    .Aggregations(a => a.Cardinality("count", c => c.Field("DeviceId.raw"))));
            if (null != response)
            {
                Console.WriteLine("Total = {0}", response.Total);
                Console.WriteLine("aggr.count = {0}", response.Aggs.Cardinality("count").Value);
            }
        }

        public static void TestAggr(IElasticClient client)
        {
            var response = client.Search<UserBehavior>(x => x
                                    .Query(q => q.Match(m => m.OnField(f => f.SystemTimeStamp).Query("1449935949")))
                                    .Aggregations(a => a.Cardinality("count", c => c.Field(f => f.UpDevice))));
            if (null != response && null != response.Documents)
            {
                Console.WriteLine("Total = {0}", response.Total);
                Console.WriteLine("Count = {0}", response.Aggs.Cardinality("count").Value);
                //foreach (var item in response.Documents)
                //    Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(item));
            }
        }

        public static void HelloWorld()
        {
            //var node = new Uri("http://localhost:9200");
            //var settings = new ConnectionSettings(node, "db_test")
            //                    .ExposeRawResponse(true);

            //var client = new ElasticClient(settings);

            // 清除数据
            //client.DeleteIndex(x => x.Index("db_test"));
            //var delResponse = client.DeleteByQuery<User>(x => x.MatchAll());
            //Console.WriteLine("Del = {0}", delResponse.Found);

            // 写入数据
            //var user = new User() { Id = Guid.NewGuid().ToString(), Name = "Hello", Description = "Hello" };
            //var index = client.Index(user);
            //Console.WriteLine("Success = {0}, Version = {1}", index.Created, index.Version);

            // 更新
            //var upResponse = client.Update<User, object>(x => x.Id("1")
            //                                  .Doc(new { Description = "Test3" })
            //                                  .Refresh());

            // 读取数据
            //var response = client.Search<User>(x => x.MatchAll());
            //var response = client.Search<User>(x => x
            //                        .From(0)
            //                        .Size(10)
            //                        .Query(q => q.Match(m => m.Query("Henr")))
            //                        .Version());
            //if (null != response.Documents)
            //    foreach (var item in response.Documents)
            //        Console.WriteLine(item);
        }
    }
}
