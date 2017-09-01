using Aliyun.MNS;
using Aliyun.MNS.Model;
using Newtonsoft.Json;

namespace HelloWeb.MessageSystem.MessageQueue
{
    public class MqClient<T> : IQueue<T>
    {
        #region Fields

        private readonly AliyunMnsSetting setting;

        private IMNS client;
        private readonly string queueName;

        #endregion

        #region Ctor

        public MqClient(AliyunMnsSetting setting)
        {
            this.setting = setting;

            // 队列名称只能由字母、数字和横线组成
            queueName = typeof(T).FullName.Replace(".", "-");

            CreateClient();
        }

        #endregion

        #region IQueue<T>

        public string Send(T message, uint? delaySeconds = null, uint? priority = null)
        {
            var json = Serialize(message);

            var request = new SendMessageRequest(json);
            if (delaySeconds.HasValue)
                request.DelaySeconds = delaySeconds.Value;
            if (priority.HasValue)
                request.Priority = priority.Value;

            var queue = GetQueue();
            var response = queue.SendMessage(request);

            return response.ToString();
        }

        public T Receive(uint? waitSeconds = null, bool deleteMessageAfterReceive = true)
        {
            var request = new ReceiveMessageRequest();
            if (waitSeconds.HasValue)
                request.WaitSeconds = waitSeconds.Value;

            var queue = GetQueue();
            var response = queue.ReceiveMessage(request);

            // 获取完信息后删除信息
            if (deleteMessageAfterReceive)
                queue.DeleteMessage(response.Message.ReceiptHandle);

            var json = response.Message.Body;
            var message = Deserialize(json);

            return message;
        }

        public T Peek()
        {
            var queue = GetQueue();
            var response = queue.PeekMessage();

            var json = response.Message.Body;
            var message = Deserialize(json);

            return message;
        }

        #endregion

        #region Utilities

        private void CreateClient()
        {
            if (null != client)
                return;

            try
            {
                client = new MNSClient(setting.AccessKey, setting.AccessSecret, setting.Endpoint);
                client.GetAccountAttributes();
            }
            catch
            {
                throw;
            }
        }

        private bool ExistsQueue()
        {
            bool exists = false;
            try
            {
                var queue = client.GetNativeQueue(queueName);
                var attributes = queue.GetAttributes();

                exists = true;
            }
            catch (QueueNotExistException) { }
            catch
            {
                throw;
            }

            return exists;
        }

        private Queue GetQueue()
        {
            if (ExistsQueue())
                return client.GetNativeQueue(queueName);
            else
                return CreateQueue();
        }

        private Queue CreateQueue()
        {
            var req = new CreateQueueRequest
            {
                QueueName = queueName,
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

        private T Deserialize(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }

        private string Serialize(T obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        #endregion
    }
}
