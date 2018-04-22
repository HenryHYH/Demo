using System;

namespace HelloWeb.MessageSystem.SDK
{
    public static class MessageSystemFactory
    {
        public static IMessageSystem Create(string url)
        {
            return new MessageSystem(new Uri(url), new AnonymousCredential());
        }
    }
}
