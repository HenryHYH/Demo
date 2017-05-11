using Microsoft.Practices.Unity.InterceptionExtension;
using Model;
using System;

namespace EntlibDemo
{
    public class UserHandler : ICallHandler
    {
        public int Order { get; set; }

        public IMethodReturn Invoke(IMethodInvocation input, GetNextHandlerDelegate getNext)
        {
            var user = input.Inputs[0] as User;

            Executing(user);
            var result = getNext()(input, getNext);
            Executed(user);

            return result;
        }

        private void Executing(User user)
        {
            Console.WriteLine("Executing");
        }

        private void Executed(User user)
        {
            Console.WriteLine("Executed");
        }
    }
}
