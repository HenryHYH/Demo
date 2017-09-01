using HelloWeb.MessageSystem.Core.Data;
using HelloWeb.MessageSystem.Core.Domain.Logging;
using HelloWeb.MessageSystem.MessageQueue;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public int BatchReceiveAndSave(uint batchSize, uint? waitSeconds = null)
        {
            var result = 0;
            var list = queue.BatchReceive(batchSize, waitSeconds);

            if (null == list || !list.Any())
                return result;

            using (var trans = new TransactionScope())
            {
                Parallel.ForEach(list, new ParallelOptions
                {
                    MaxDegreeOfParallelism = 3
                }, x =>
                {
                    Insert(x);
                });

                trans.Complete();
                result = list.Count;
            }

            return result;
        }
    }
}
