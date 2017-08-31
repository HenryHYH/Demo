using System;
using System.Threading;

namespace HelloWeb.MessageSystem.MessageQueue
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Start");

            var setting = new AliyunMnsSetting
            {
                AccessKey = "LTAIAEMVSMtDaRcn",
                AccessSecret = "u6CgiFiWd6ahL8ux4fRd7tWmiHmDhH",
                Endpoint = "https://1352702786563330.mns.cn-hangzhou.aliyuncs.com/"
            };
            IQueue queue = new MqClient(setting);
            var response = queue.Send("Helloworld", "ABC");
            Console.WriteLine($"res = {response}");

            Thread.Sleep(3000);

            var message = queue.Receive("Helloworld");
            Console.WriteLine($"message = {message}");

            Console.WriteLine("Finish");
            Console.ReadKey();
        }
    }
}
