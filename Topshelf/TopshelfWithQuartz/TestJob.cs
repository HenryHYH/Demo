using Quartz;
using System;

namespace TopshelfWithQuartz
{
    public class TestJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            Console.WriteLine("{0:yyyy-MM-dd HH:mm:ss.ffff}", DateTime.Now);
        }
    }
}
