using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CoolBuyer.Server.BusinessLogic.Common.Exceptions
{
    public class AccountNotConfirmedException : Exception
    {
        public AccountNotConfirmedException()
            : base()
        {
        }

        public AccountNotConfirmedException(string message)
            : base(message)
        {
        }

        public AccountNotConfirmedException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected AccountNotConfirmedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
