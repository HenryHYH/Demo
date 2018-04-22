using Aliyun.Api.LOG;
using Aliyun.Api.LOG.Data;
using Aliyun.Api.LOG.Request;
using Aliyun.Api.LOG.Response;
using Aliyun.Api.SLS.Response;
using System;
using System.Collections.Generic;

namespace HelloWeb.MessageSystem.LogService
{
    class Program
    {
        static void Main(string[] args)
        {
            // Test2();
            Test3();
        }

        private static void Test3()
        {
            string endpoint = "http://cn-shenzhen.log.aliyuncs.com"; //选择与上面步骤创建 project 所属区域匹配的日志服务 Endpoint
            string accessKeyId = "LTAIAEMVSMtDaRcn";  //使用你的阿里云访问秘钥 AccessKeyId
            string accessKeySecret = "u6CgiFiWd6ahL8ux4fRd7tWmiHmDhH"; //使用你的阿里云访问秘钥 AccessKeySecret
            string project = "henry-hyh-logservice";      //上面步骤创建的项目名称
            string logstore = "henry-hyh-logstore";    //上面步骤创建的日志库名称

            int shardId = 0;
            LogClient client = new LogClient(endpoint, accessKeyId, accessKeySecret);
            //init http connection timeout
            client.ConnectionTimeout = client.ReadWriteTimeout = 10000;
            //*
            //list logstores
            foreach (String l in client.ListLogstores(new ListLogstoresRequest(project)).Logstores)
            {
                Console.WriteLine(l);
            }
            //put logs
            DateTime unixTimestampZeroPoint = new DateTime(1970, 01, 01, 0, 0, 0, DateTimeKind.Utc);
            PutLogsRequest putLogsReqError = new PutLogsRequest();
            putLogsReqError.Project = project;
            putLogsReqError.Topic = "dotnet_topic";
            putLogsReqError.Logstore = logstore;
            putLogsReqError.LogItems = new List<LogItem>();
            for (int i = 1; i <= 10; ++i)
            {
                LogItem logItem = new LogItem();
                logItem.Time = (uint)((DateTime.UtcNow - unixTimestampZeroPoint).TotalSeconds);
                for (int k = 0; k < 10; ++k)
                    logItem.PushBack("error_", "invalid operation_" + i + "_" + k);
                putLogsReqError.LogItems.Add(logItem);
            }
            PutLogsResponse putLogRespError = client.PutLogs(putLogsReqError);
            Console.WriteLine("RequestId = {0}", putLogRespError.GetRequestId());

            DateTime fromStamp = DateTime.UtcNow - new TimeSpan(0, 10, 0);
            DateTime toStamp = DateTime.UtcNow;
            uint from = (uint)((fromStamp - unixTimestampZeroPoint).TotalSeconds);
            uint to = (uint)((toStamp - unixTimestampZeroPoint).TotalSeconds);

            GetCursorResponse cursorResponse = client.GetCursor(new GetCursorRequest(project, logstore, shardId, ShardCursorMode.BEGIN));
            var beginCursor = cursorResponse.Cursor;
            cursorResponse = client.GetCursor(new GetCursorRequest(project, logstore, shardId, ShardCursorMode.END));
            var endCursor = cursorResponse.Cursor;
            var curCusor = beginCursor;
            while (curCusor != endCursor)
            {
                BatchGetLogsResponse batchResponse = client.BatchGetLogs(new BatchGetLogsRequest(project, logstore, shardId, curCusor, 10));
                curCusor = batchResponse.NextCursor;
                var logGroups = batchResponse.LogGroupList;
                foreach (var logGroup in logGroups.LogGroupList_List)
                {
                    Console.WriteLine("Source = " + logGroup.Source);
                    Console.WriteLine("Topic = " + logGroup.Topic);
                    foreach (var log in logGroup.LogsList)
                    {
                        Console.WriteLine("Time = " + log.Time);
                        foreach (var content in log.ContentsList)
                        {
                            Console.WriteLine("{0} : {1}", content.Key, content.Value);
                        }
                    }
                }
            }

            Console.WriteLine("Finish");
            Console.ReadKey();
        }

        private static void Test2()
        {
            string endpoint = "http://cn-shenzhen.log.aliyuncs.com"; //选择与上面步骤创建 project 所属区域匹配的日志服务 Endpoint
            string accessKeyId = "LTAIAEMVSMtDaRcn";  //使用你的阿里云访问秘钥 AccessKeyId
            string accessKeySecret = "u6CgiFiWd6ahL8ux4fRd7tWmiHmDhH"; //使用你的阿里云访问秘钥 AccessKeySecret
            string project = "henry-hyh-logservice";      //上面步骤创建的项目名称
            string logstore = "henry-hyh-logstore";    //上面步骤创建的日志库名称

            //构建一个客户端实例
            LogClient client = new LogClient(endpoint, accessKeyId, accessKeySecret);
            //列出当前 project 下的所有日志库名称
            ListLogstoresResponse res1 = client.ListLogstores(new ListLogstoresRequest(project));
            Console.WriteLine("Totoal logstore number is " + res1.Count);
            foreach (string name in res1.Logstores)
            {
                Console.WriteLine(name);
            }

            DateTime unixTimestampZeroPoint = new DateTime(1970, 01, 01, 0, 0, 0, DateTimeKind.Utc);
            //写入日志
            List<LogItem> logs = new List<LogItem>();
            for (int i = 0; i < 5; i++)
            {
                LogItem item = new LogItem();
                item.Time = (uint)((DateTime.UtcNow - unixTimestampZeroPoint).TotalSeconds);
                item.PushBack("CTime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff", System.Globalization.DateTimeFormatInfo.InvariantInfo));
                item.PushBack("Index", i.ToString());
                logs.Add(item);
            }
            string topic = "hello.topic";    //选择合适的日志主题
            string source = "localhost";    //选择合适的日志来源（如 IP 地址）
            PutLogsResponse res4 = client.PutLogs(new PutLogsRequest(project, logstore, topic, source, logs));
            Console.WriteLine("Request ID for PutLogs: " + res4.GetRequestId());

            //等待 1 分钟让日志可查询
            // System.Threading.Thread.Sleep(60 * 1000);
            System.Threading.Thread.Sleep(5 * 1000);

            DateTime fromStamp = DateTime.UtcNow - new TimeSpan(0, 10, 0);
            DateTime toStamp = DateTime.UtcNow;
            uint from = (uint)((fromStamp - unixTimestampZeroPoint).TotalSeconds);
            uint to = (uint)((toStamp - unixTimestampZeroPoint).TotalSeconds);
            var query = "Index";

            //查询日志分布情况
            GetHistogramsResponse res2 = null;
            do
            {
                res2 = client.GetHistograms(new GetHistogramsRequest(project, logstore, from, to));
            } while ((res2 != null) && (!res2.IsCompleted()));
            Console.WriteLine("Total count of logs is " + res2.TotalCount);
            foreach (Histogram ht in res2.Histograms)
            {
                Console.WriteLine(string.Format("from {0}, to {1}, count {2}.", ht.From, ht.To, ht.Count));
            }

            //查询日志数据
            GetLogsResponse res3 = null;
            do
            {
                res3 = client.GetLogs(new GetLogsRequest(project, logstore, from, to, string.Empty, query, 5, 0, true));
            } while ((res3 != null) && (!res3.IsCompleted()));
            Console.WriteLine("Have gotten logs...");
            foreach (QueriedLog log in res3.Logs)
            {
                Console.WriteLine("----{0}, {1}---", log.Time, log.Source);
                for (int i = 0; i < log.Contents.Count; i++)
                {
                    Console.WriteLine("{0} --> {1}", log.Contents[i].Key, log.Contents[i].Value);
                }
            }

            Console.WriteLine("Finish");
            Console.ReadKey();
        }


        private static void Test()
        {
            // string endpoint = "http://cn-hangzhou-failover-intranet.sls.aliyuncs.com",
            string endpoint = "http://cn-shenzhen.log.aliyuncs.com",
                accesskeyId = "LTAIAEMVSMtDaRcn",
                accessKey = "u6CgiFiWd6ahL8ux4fRd7tWmiHmDhH",
                project = "henry-hyh-logservice",
                logstore = "henry-hyh-logstore";
            int shardId = 0;
            LogClient client = new LogClient(endpoint, accesskeyId, accessKey);
            //init http connection timeout
            client.ConnectionTimeout = client.ReadWriteTimeout = 10000;
            //list logstores
            foreach (string l in client.ListLogstores(new ListLogstoresRequest(project)).Logstores)
            {
                Console.WriteLine(l);
            }
            //put logs
            PutLogsRequest putLogsReqError = new PutLogsRequest();
            putLogsReqError.Project = project;
            putLogsReqError.Topic = "dotnet_topic";
            putLogsReqError.Logstore = logstore;
            putLogsReqError.LogItems = new List<LogItem>();
            DateTime unixTimestampZeroPoint = new DateTime(1970, 01, 01, 0, 0, 0, DateTimeKind.Utc);
            for (int i = 1; i <= 10; ++i)
            {
                LogItem logItem = new LogItem();
                logItem.Time = (uint)((DateTime.UtcNow - unixTimestampZeroPoint).TotalSeconds);
                for (int k = 0; k < 10; ++k)
                    logItem.PushBack("error_", "invalid operation");
                putLogsReqError.LogItems.Add(logItem);
            }
            PutLogsResponse putLogRespError = client.PutLogs(putLogsReqError);

            DateTime fromStamp = DateTime.UtcNow - new TimeSpan(0, 10, 0);
            DateTime toStamp = DateTime.UtcNow;
            uint from = (uint)((fromStamp - unixTimestampZeroPoint).TotalSeconds);
            uint to = (uint)((toStamp - unixTimestampZeroPoint).TotalSeconds);
            //query logs
            client.GetLogs(new GetLogsRequest(project, logstore, from, to));
            //query histogram
            client.GetHistograms(new GetHistogramsRequest(project, logstore, from, to));
            //list shards
            client.ListShards(new ListShardsRequest(project, logstore));
            //get cursor
            string cursor = client.GetCursor(new GetCursorRequest(project, logstore, shardId, ShardCursorMode.BEGIN)).Cursor;
            Console.WriteLine(cursor);
            //batch get logs, loghub interface
            BatchGetLogsResponse response = client.BatchGetLogs(new BatchGetLogsRequest(project, logstore, shardId, cursor, 10));

            //list topic
            client.ListTopics(new ListTopicsRequest(project, logstore));

            Console.WriteLine("Finish");
            Console.ReadKey();
        }
    }
}
