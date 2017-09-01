using System;
using System.Collections.Generic;
using System.Threading;

namespace HelloWeb.MessageSystem.MessageQueue
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Start");

            Test();
            //TestBatch();

            Console.WriteLine("Finish");
            Console.ReadKey();
        }

        private static void Test()
        {
            var setting = new AliyunMnsSetting
            {
                AccessKey = "LTAIAEMVSMtDaRcn",
                AccessSecret = "u6CgiFiWd6ahL8ux4fRd7tWmiHmDhH",
                Endpoint = "https://1352702786563330.mns.cn-hangzhou.aliyuncs.com/"
            };
            IQueue<TestData> queue = new MqClient<TestData>(setting);

            var data = new TestData
            {
                Id = 1,
                Name = "Hello world",
                CTime = DateTime.Now
            };
            var response = queue.Send(data);
            Console.WriteLine($"res = {response}");

            Thread.Sleep(3000);

            var message = queue.Receive();
            Console.WriteLine($"message = {message}");

            //var peekMessage = queue.Peek();
            //Console.WriteLine($"peekMessage = {peekMessage}");
        }

        private static void TestBatch()
        {
            var setting = new AliyunMnsSetting
            {
                AccessKey = "LTAIAEMVSMtDaRcn",
                AccessSecret = "u6CgiFiWd6ahL8ux4fRd7tWmiHmDhH",
                Endpoint = "https://1352702786563330.mns.cn-hangzhou.aliyuncs.com/"
            };
            IQueue<TestData> queue = new MqClient<TestData>(setting);

            var list = new List<TestData>();
            for (int i = 0; i < 4; i++)
                list.Add(new TestData
                {
                    Id = i,
                    Name = $"Name_{i}",
                    CTime = DateTime.Now
                });

            var count = queue.BatchSend(list);
            Console.WriteLine($"Count = {count}");

            Thread.Sleep(3000);

            var messages = queue.BatchReceive(16);
            Console.WriteLine($"Count = {messages.Count}");
        }
    }
}
