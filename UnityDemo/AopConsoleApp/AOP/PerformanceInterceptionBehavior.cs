using Microsoft.Practices.Unity.InterceptionExtension;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AopConsoleApp.AOP
{
    public class PerformanceInterceptionBehavior : IInterceptionBehavior
    {
        public bool WillExecute
        {
            get { return true; }
        }

        public IEnumerable<Type> GetRequiredInterfaces()
        {
            return Type.EmptyTypes;
        }

        public IMethodReturn Invoke(IMethodInvocation input, GetNextInterceptionBehaviorDelegate getNext)
        {
            WriteLog(string.Format("Performance Interception invoking method {0}", input.MethodBase));

            var sw = new Stopwatch();
            sw.Start();

            // Invoke
            var result = getNext()(input, getNext);

            sw.Stop();

            if (null != result.Exception)
            {
                WriteLog(string.Format("{0} throw exception {1}", input.MethodBase, result.Exception.Message));
            }
            else
            {
                WriteLog(string.Format("{0} executed {1}", input.MethodBase, sw.Elapsed));
            }

            return result;
        }

        private void WriteLog(string message)
        {
            Console.WriteLine("{0:yyyy-MM-dd HH:mm:ss.ffff} - {1}", DateTime.Now, message);
        }
    }
}
