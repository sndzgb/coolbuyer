using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CoolBuyer.Server.BusinessLogic.Common.Exceptions
{
    public class ModelValidationException : Exception
    {
        public ModelValidationException()
            : base()
        {
        }

        public ModelValidationException(string message)
            : base(message)
        {
        }

        public ModelValidationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected ModelValidationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
