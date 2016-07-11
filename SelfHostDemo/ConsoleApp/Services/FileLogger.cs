using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using log4net;

namespace ConsoleApp.Services
{
    public class FileLogger : ILogger
    {
        #region Fields

        private static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        #endregion

        #region Methods

        public void Debug(string message, Exception ex = null)
        {
            logger.Debug(message, ex);
        }

        #endregion
    }
}
