using System;
using System.Threading;

namespace HelloWeb.MessageSystem.MessageQueue
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Test();
        }

        private static void Test()
        {
            Console.WriteLine("Start");

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

            Console.WriteLine("Finish");
            Console.ReadKey();
        }
    }
}
