using Aliyun.MNS;
using Aliyun.MNS.Model;

namespace HelloWeb.MessageSystem.MessageQueue
{
    public class MqClient : IQueue
    {
        private readonly AliyunMnsSetting setting;

        private IMNS client;

        public MqClient(AliyunMnsSetting setting)
        {
            this.setting = setting;
            CreateClient();
        }

        public Queue CreateQueue(string names)
        {
            var req = new CreateQueueRequest
            {
                QueueName = names,
                Attributes =
                {
                    DelaySeconds = 10,
                    VisibilityTimeout = 30,
                    MaximumMessageSize = 40960,
                    MessageRetentionPeriod = 345600,
                    PollingWaitSeconds = 15
                }
            };

            Queue queue = null;
            try
            {
                queue = client.CreateQueue(req);
            }
            catch
            {
                throw;
            }

            return queue;
        }

        public string Send(string queueName, string message)
        {
            var queue = client.GetNativeQueue(queueName);
            var request = new SendMessageRequest(message)
            {
                DelaySeconds = 2
            };
            var response = queue.SendMessage(request);

            return response.ToString();
        }

        public string Receive(string queueName)
        {
            var queue = client.GetNativeQueue(queueName);
            var response = queue.ReceiveMessage(5);

            queue.DeleteMessage(response.Message.ReceiptHandle);

            return response.Message.Body;
        }

        private void CreateClient()
        {
            client = new MNSClient(setting.AccessKey, setting.AccessSecret, setting.Endpoint);

            try
            {
                client.GetAccountAttributes();
            }
            catch
            {
                throw;
            }
        }
    }
}
