using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CoolBuyer.Server.BusinessLogic.Common.Exceptions
{
    public class OperationNotAllowedException : Exception
    {
        public OperationNotAllowedException()
            : base()
        {
        }

        public OperationNotAllowedException(string message)
            : base(message)
        {
        }

        public OperationNotAllowedException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected OperationNotAllowedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
