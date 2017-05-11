using System;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;

namespace RealProxyDemo
{
    public class ServiceProxy<T> : RealProxy
    {
        private T target;

        public ServiceProxy(T target) : base(typeof(T))
        {
            this.target = target;
        }

        public override IMessage Invoke(IMessage msg)
        {
            Executing(msg);

            IMethodCallMessage callMessage = (IMethodCallMessage)msg;
            object returnValue = callMessage.MethodBase.Invoke(target, callMessage.Args);

            Executed(msg);

            return new ReturnMessage(returnValue, new object[0], 0, null, callMessage);
        }

        private void Executing(IMessage msg)
        {
            Console.WriteLine("Executing");
        }

        private void Executed(IMessage msg)
        {
            Console.WriteLine("Executed");
        }
    }

    public static class TransparentProxy
    {
        public static T Create<T>()
        {
            T instance = Activator.CreateInstance<T>();
            ServiceProxy<T> proxy = new ServiceProxy<T>(instance);
            T transparentProxy = (T)proxy.GetTransparentProxy();

            return transparentProxy;
        }
    }
}
