using Model;
using Shuttle.Core.Infrastructure;
using Shuttle.ESB.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class NoticeHandler : IMessageHandler<Notice>
    {
        public void ProcessMessage(HandlerContext<Notice> context)
        {
            ColoredConsole.WriteLine(ConsoleColor.Magenta,
                "{0} - {1:yyyy-MM-dd hh:mm:ss}", context.Message.Message, context.Message.CreateTime);
        }

        public bool IsReusable
        {
            get { return true; }
        }
    }
}
