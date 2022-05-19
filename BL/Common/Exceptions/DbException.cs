using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CoolBuyer.Server.BusinessLogic.Common.Exceptions
{
    public class DbException : Exception
    {
        public DbException()
            : base()
        {
        }

        public DbException(string message)
            : base(message)
        {
        }

        public DbException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected DbException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
