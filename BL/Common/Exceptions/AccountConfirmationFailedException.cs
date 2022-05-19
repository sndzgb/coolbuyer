using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CoolBuyer.Server.BusinessLogic.Common.Exceptions
{
    public class AccountConfirmationFailedException : Exception
    {
        public AccountConfirmationFailedException()
            : base()
        {
        }

        public AccountConfirmationFailedException(string message)
            : base(message)
        {
        }

        public AccountConfirmationFailedException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected AccountConfirmationFailedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
