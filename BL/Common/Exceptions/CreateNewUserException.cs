using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CoolBuyer.Server.BusinessLogic.Common.Exceptions
{
    public class CreateNewUserException : Exception
    {
        public CreateNewUserException()
            : base()
        {
        }

        public CreateNewUserException(string message)
            : base(message)
        {
        }

        public CreateNewUserException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected CreateNewUserException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
