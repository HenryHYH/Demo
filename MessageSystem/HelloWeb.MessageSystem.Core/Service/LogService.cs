using HelloWeb.MessageSystem.Core.Data;
using HelloWeb.MessageSystem.Core.Domain.Logging;
using HelloWeb.MessageSystem.MessageQueue;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;

namespace HelloWeb.MessageSystem.Core.Service
{
    public class LogService : ILogService
    {
        private readonly IBaseRepository<Log> repository;
        private readonly IQueue<Log> queue;

        public LogService(IBaseRepository<Log> repository,
                            IQueue<Log> queue)
        {
            this.repository = repository;
            this.queue = queue;
        }

        public void Insert(Log log)
        {
            repository.Insert(log);
        }

        public IEnumerable<Log> Search(string projectName)
        {
            return repository.Find(x => x.ProjectName == projectName);
        }

        public bool Send(Log log)
        {
            return queue.Send(log);
        }

        public int ReceiveAndSave(uint batchSize)
        {
            var list = queue.BatchReceive(batchSize);

            if (null == list || !list.Any())
                return 0;

            var result = 0;
            using (var trans = new TransactionScope())
            {
                foreach (var item in list)
                    Insert(item);

                trans.Complete();
                result = list.Count;
            }

            return result;
        }
    }
}
