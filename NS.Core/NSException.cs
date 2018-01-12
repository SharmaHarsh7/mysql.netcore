using System;
using System.Runtime.Serialization;

namespace NS.Core
{
    [Serializable]
    public class NSException : Exception
    {
        public NSException()
        {
        }
        
        public NSException(string message)
            : base(message)
        {
        }

        public NSException(string messageFormat, params object[] args)
            : base(string.Format(messageFormat, args))
        {
        }

        protected NSException(SerializationInfo
            info, StreamingContext context)
            : base(info, context)
        {
        }
        public NSException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
