using FW.Core.Domain.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FW.Service.Logging
{
    public interface ILogger
    {
        Log Insert(LogLevel level, string shortMessage, string fullMessage = "");
    }
}
