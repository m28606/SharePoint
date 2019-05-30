using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace TPCIP.CommonDomain
{
    [Serializable]
    public class FriendlyException : Exception
    {
        protected FriendlyException(SerializationInfo info, StreamingContext context) 
            : base(info, context)
        {
        }

        public FriendlyException(string message) : base(message)
        {
            
        }

        public FriendlyException(string message, Exception innerException) : base(message, innerException)
        {
            
        }
    }
}
