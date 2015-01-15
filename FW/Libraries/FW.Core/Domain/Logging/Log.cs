namespace FW.Core.Domain.Logging
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Log : BaseEntity
    {
        #region Properties

        public DateTime CreateTime
        {
            get; set;
        }

        public string FullMessage
        {
            get; set;
        }

        public LogLevel LogLevel
        {
            get; set;
        }

        public string ShortMessage
        {
            get; set;
        }

        #endregion Properties
    }
}