using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helloworld.Jobs
{
    public class ShowTimeJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            Console.WriteLine(string.Format("{0:yyyy-MM-dd HH:mm:ss.ffff}", DateTime.Now));
        }
    }
}
