using System.Runtime.Serialization;
using Xunit.Sdk;

namespace XunitShould.Sdk
{
    internal class XunitException : AssertException
    {
        public XunitException() { }

        public XunitException(string message)
            : base(message) { }

        protected XunitException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}