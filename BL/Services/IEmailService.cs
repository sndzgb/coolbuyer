using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolBuyer.Server.BusinessLogic.Services
{
    public interface IEmailService
    {
        void Send(EmailMessage email);
        Task SendAsync(EmailMessage email);
    }

    public class EmailMessage
    {
        public string FromAddress { get; set; }
        public string ToAddress { get; set; }

        //public string Destination { get; set; }
        public string Body { get; set; }
        public string Subject { get; set; }
    }
}
