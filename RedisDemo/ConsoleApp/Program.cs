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
            // TestPubSub();
            // TestPubSub2();
            TestPubSub3();

            Console.ReadKey(true);
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
        }

        private static void TestPubSub3()
        {
            string host = "127.0.0.1";
            int port = 6379;
            var ChannelName = "Channel";

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
                };
                subscription.SubscribeToChannels(ChannelName); //blocking
            }

            Console.WriteLine("EOF");
        }

        private static void TestPubSub2()
        {
            int noOfClients = 1;
            string host = "127.0.0.1";
            int port = 6379;
            var ChannelName = "Channel";
            // var ChannelName = "__keyspace@0__:foo";
            var MessagePrefix = "MessagePrefix_";
            var PublishMessageCount = 5;

            for (var i = 1; i <= noOfClients; i++)
            {
                var clientNo = i;
                ThreadPool.QueueUserWorkItem(x =>
                {
                    using (var redisConsumer = new RedisClient(host, port))
                    using (var subscription = redisConsumer.CreateSubscription())
                    {
                        var messagesReceived = 0;
                        //subscription.OnSubscribe = channel =>
                        //{
                        //    Console.WriteLine("Client #{0} Subscribed to '{1}'", clientNo, channel);
                        //};
                        //subscription.OnUnSubscribe = channel =>
                        //{
                        //    Console.WriteLine("Client #{0} UnSubscribed from '{1}'", clientNo, channel);
                        //};
                        subscription.OnMessage += (channel, msg) =>
                        {
                            Console.WriteLine("Client #{0} Received '{1}' from channel '{2}'",
                                clientNo, msg, channel);

                            if (++messagesReceived == PublishMessageCount)
                            {
                                // subscription.UnSubscribeFromAllChannels();
                            }
                        };
                        subscription.OnMessage += (channel, msg) =>
                        {
                            Console.WriteLine("Test - " + msg);
                        };

                        Console.WriteLine("Client #{0} started Listening On '{1}'", clientNo, ChannelName);
                        subscription.SubscribeToChannels(ChannelName); //blocking
                        // subscription.SubscribeToChannelsMatching("__keyspace@*__:Prefix_*");
                    }

                    Console.WriteLine("Client #{0} EOF", clientNo);
                });
            }

            using (var redisClient = new RedisClient(host, port))
            {
                Thread.Sleep(500);
                Console.WriteLine("Begin publishing messages...");

                //for (var i = 1; i <= PublishMessageCount; i++)
                //{
                //    var message = MessagePrefix + i;
                //    Console.WriteLine("Publishing '{0}' to '{1}'", message, ChannelName);
                //    redisClient.PublishMessage(ChannelName, message);
                //}


                string message = Console.ReadLine();
                while (!string.IsNullOrWhiteSpace(message))
                {
                    redisClient.PublishMessage(ChannelName, message);
                    message = Console.ReadLine();
                }
            }

            // Thread.Sleep(500);

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
