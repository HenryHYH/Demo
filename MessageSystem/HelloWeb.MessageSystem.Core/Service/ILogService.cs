using HelloWeb.MessageSystem.Core.Domain.Logging;
using System.Collections.Generic;

namespace HelloWeb.MessageSystem.Core.Service
{
    public interface ILogService
    {
        void Insert(Log log);

        IEnumerable<Log> Search(string projectName);

        bool Send(Log log);

        int BatchReceiveAndSave(uint batchSize, uint? waitSeconds = null);
    }
}
