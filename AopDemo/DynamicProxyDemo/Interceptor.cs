using Castle.DynamicProxy;
using System;

namespace DynamicProxyDemo
{
    public class Interceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            Executing(invocation);
            invocation.Proceed();
            Executed(invocation);
        }

        private void Executing(IInvocation invocation)
        {
            Console.WriteLine("Executing");
        }

        private void Executed(IInvocation invocation)
        {
            Console.WriteLine("Executed");
        }
    }
}
