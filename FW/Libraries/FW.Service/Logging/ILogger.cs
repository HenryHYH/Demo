namespace FW.Service.Logging
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using FW.Core.Domain.Logging;

    public interface ILogger
    {
        #region Methods

        Log Insert(LogLevel level, string shortMessage, string fullMessage = "");

        #endregion Methods
    }
}