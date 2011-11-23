using System;
using System.Collections.Generic;
using System.Text;

namespace Itop.Client.Common
{
    public class ItopClientException : Exception
    {
        public ItopClientException()
            : base()
        {
        }

        public ItopClientException(string message)
            : base(message)
        {
        }

        public ItopClientException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
