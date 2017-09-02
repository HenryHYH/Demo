using NUnit.Framework;

namespace HelloWeb.MessageSystem.MessageQueue.Tests
{
    public class QueueTest
    {
        private IQueue<QueueTestData> queue;

        [SetUp]
        public void SetUp()
        {
            var setting = new AliyunMnsSetting
            {
                AccessKey = "LTAIAEMVSMtDaRcn",
                AccessSecret = "u6CgiFiWd6ahL8ux4fRd7tWmiHmDhH",
                Endpoint = "https://1352702786563330.mns.cn-hangzhou.aliyuncs.com/"
            };
            queue = new MqClient<QueueTestData>(setting);
        }

        [Test]
        public void Test_Receive()
        {
            var data = queue.Receive();
            Assert.Null(data);
        }

        [Test]
        public void Test_BatchReceive()
        {
            var data = queue.BatchReceive(16);
            Assert.Zero(data.Count);
        }
    }
}
