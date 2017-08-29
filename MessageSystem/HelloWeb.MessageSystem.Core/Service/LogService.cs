using HelloWeb.MessageSystem.Core.Data;
using HelloWeb.MessageSystem.Core.Domain.Logging;
using System.Collections.Generic;

namespace HelloWeb.MessageSystem.Core.Service
{
    public class LogService : ILogService
    {
        private IBaseRepository<Log> repository;

        public LogService(IBaseRepository<Log> repository)
        {
            this.repository = repository;
        }

        public void Insert(Log log)
        {
            repository.Insert(log);
        }

        public IEnumerable<Log> Search(string projectName)
        {
            return repository.Find(x => x.ProjectName == projectName);
        }
    }
}
