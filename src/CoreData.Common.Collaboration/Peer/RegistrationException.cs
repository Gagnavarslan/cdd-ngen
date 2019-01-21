using System;
using System.Runtime.Serialization;

namespace CoreData.Common.Messaging.Peer
{
    public class RegistrationException : ApplicationException
    {
        public RegistrationException() { }

        public RegistrationException(string message) : base(message) { }

        public RegistrationException(string message, Exception innerException)
            : base(message, innerException) { }

        protected RegistrationException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}
