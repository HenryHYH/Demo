using Aliyun.MNS;
using Aliyun.MNS.Model;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace HelloWeb.MessageSystem.MessageQueue
{
    public class MqClient<T> : IQueue<T>
        where T : class
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

        public bool Send(T message, uint? delaySeconds = null, uint? priority = null)
        {
            if (null == message)
                return false;

            var json = Serialize(message);

            var request = new SendMessageRequest(json);
            if (delaySeconds.HasValue)
                request.DelaySeconds = delaySeconds.Value;
            if (priority.HasValue)
                request.Priority = priority.Value;

            var queue = GetQueue();
            var response = queue.SendMessage(request);

            return null != response &&
                    response.HttpStatusCode == System.Net.HttpStatusCode.Created;
        }

        public T Receive(uint? waitSeconds = null, bool deleteMessageAfterReceive = true)
        {
            T message = default(T);

            var request = new ReceiveMessageRequest();
            if (waitSeconds.HasValue)
                request.WaitSeconds = waitSeconds.Value;

            var queue = GetQueue();
            ReceiveMessageResponse response = null;
            try
            {
                response = queue.ReceiveMessage(request);
            }
            catch (MessageNotExistException) { }

            if (null != response)
            {
                // 处理数据
                message = Deserialize(response.Message.Body);

                // 获取完信息后删除信息
                if (deleteMessageAfterReceive)
                    queue.DeleteMessage(response.Message.ReceiptHandle);
            }

            return message;
        }

        public T Peek()
        {
            T message = default(T);

            var queue = GetQueue();
            var response = queue.PeekMessage();

            if (null != response)
                message = Deserialize(response.Message.Body);

            return message;
        }

        public int BatchSend(ICollection<T> messages, uint? delaySeconds = null, uint? priority = null)
        {
            if (null == messages || !messages.Any())
                throw new System.ArgumentNullException("messages", "messages不能为空");
            else if (16 < messages.Count)
                throw new System.ArgumentOutOfRangeException("messages", "messages最大记录数为16");

            var requests = new List<SendMessageRequest>();
            foreach (var message in messages)
            {
                var json = Serialize(message);
                var item = new SendMessageRequest(json);
                if (delaySeconds.HasValue)
                    item.DelaySeconds = delaySeconds.Value;
                if (priority.HasValue)
                    item.Priority = priority.Value;

                requests.Add(item);
            }
            var request = new BatchSendMessageRequest { Requests = requests };

            var queue = GetQueue();
            var response = queue.BatchSendMessage(request);

            return null == response ? 0 : response.Responses.Count;
        }

        public ICollection<T> BatchReceive(uint batchSize, uint? waitSeconds = null, bool deleteMessageAfterReceive = true)
        {
            if (16 < batchSize)
                throw new System.ArgumentOutOfRangeException("batchSize", "batchSize最大为16");

            var list = new List<T>();

            var request = new BatchReceiveMessageRequest(batchSize);
            if (waitSeconds.HasValue)
                request.WaitSeconds = waitSeconds.Value;

            var queue = GetQueue();
            BatchReceiveMessageResponse response = null;
            try
            {
                response = queue.BatchReceiveMessage(request);
            }
            catch (MessageNotExistException) { }

            if (null != response &&
                0 < response.Messages.Count)
            {
                // 处理数据
                list = response.Messages.Select(x => Deserialize(x.Body)).ToList();

                // 获取完信息后删除信息
                if (deleteMessageAfterReceive)
                {
                    var receiptHandles = response.Messages.Select(x => x.ReceiptHandle).ToList();
                    var deleteRequest = new BatchDeleteMessageRequest
                    {
                        ReceiptHandles = receiptHandles
                    };
                    queue.BatchDeleteMessage(deleteRequest);
                }
            }

            return list;
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
