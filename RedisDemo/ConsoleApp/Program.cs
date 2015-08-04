using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ServiceStack.Redis;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Helloworld();
            TestPubSub();
        }

        private static void TestPubSub()
        {
            string host = "127.0.0.1";
            int port = 6379;
            var messagesReceived = 0;
            var ChannelName = "Channel";
            var MessagePrefix = "MessagePrefix_";
            var PublishMessageCount = 5;

            using (var redisConsumer = new RedisClient(host, port))
            using (var subscription = redisConsumer.CreateSubscription())
            {
                subscription.OnSubscribe = channel =>
                {
                    Console.WriteLine("Subscribed to '{0}'", channel);
                };
                subscription.OnUnSubscribe = channel =>
                {
                    Console.WriteLine("UnSubscribed from '{0}'", channel);
                };
                subscription.OnMessage = (channel, msg) =>
                {
                    Console.WriteLine("Received '{0}' from channel '{1}'", msg, channel);

                    //As soon as we've received all 5 messages, disconnect by unsubscribing to all channels
                    if (++messagesReceived == PublishMessageCount)
                    {
                        subscription.UnSubscribeFromAllChannels();
                    }
                };

                ThreadPool.QueueUserWorkItem(x =>
                {
                    Thread.Sleep(200);
                    Console.WriteLine("Begin publishing messages...");

                    using (var redisPublisher = new RedisClient(host, port))
                    {
                        for (var i = 1; i <= PublishMessageCount; i++)
                        {
                            var message = MessagePrefix + i;
                            Console.WriteLine("Publishing '{0}' to '{1}'", message, ChannelName);
                            redisPublisher.PublishMessage(ChannelName, message);
                        }
                    }
                });

                Console.WriteLine("Started Listening On '{0}'", ChannelName);
                subscription.SubscribeToChannels(ChannelName); //blocking
            }

            Console.WriteLine("EOF");
            Console.ReadKey(true);
        }

        private static void Helloworld()
        {
            string host = "127.0.0.1";
            int port = 6379;

            using (var client = new RedisClient(host, port))
            {
                var hello = client.Get<string>("hello");
                if (string.IsNullOrWhiteSpace(hello))
                {
                    client.Set<string>("hello", "henry");
                    Console.WriteLine("Write");
                }
                else
                    Console.WriteLine(hello);
            }

            Console.ReadLine();
        }
    }
}
