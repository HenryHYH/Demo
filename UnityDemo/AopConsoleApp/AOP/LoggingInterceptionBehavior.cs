using Microsoft.Practices.Unity.InterceptionExtension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AopConsoleApp.AOP
{
    public class LoggingInterceptionBehavior : IInterceptionBehavior
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
            WriteLog("Logging Interception invoking method {0}", input.MethodBase);

            // Invoke
            var result = getNext()(input, getNext);

            if (null != result.Exception)
            {
                WriteLog("{0} throw exception {1}", input.MethodBase,
                                                    result.Exception.Message);
            }
            else
            {
                WriteLog("{0} return {1}", input.MethodBase,
                                           result.ReturnValue);
            }

            return result;
        }

        private void WriteLog(string message)
        {
            Console.WriteLine("{0:yyyy-MM-dd HH:mm:ss.ffff} - {1}", DateTime.Now, message);
        }

        private void WriteLog(string format, params object[] args)
        {
            WriteLog(string.Format(format, args));
        }
    }
}
