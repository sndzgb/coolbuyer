using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace CoolBuyer.Server.BusinessLogic.Services
{
    public class EmailMessagingService : IEmailService
    {
        public EmailMessagingService()
        {
        }


        public void Send(EmailMessage email)
        {
            if (email == null)
            {
                throw new ArgumentNullException("email");
            }

            using (var client = 
                new SmtpClient("localhost")
                {
                    EnableSsl = false,
                    Port = 25,
                    DeliveryMethod = SmtpDeliveryMethod.Network
                })
            {
                using (var emailMsg =
                    new MailMessage(
                        new MailAddress("noreply@mydomain.com", "(do not reply)"), //add option to "(EmailMessage) email"
                        new MailAddress(email.ToAddress))
                    {
                        Subject = email.Subject,
                        Body = email.Body,
                        IsBodyHtml = true
                    })
                {
                    client.Send(emailMsg);
                }
            }
        }
        

        public Task SendAsync(EmailMessage email)
        {
            return Task.Run(() => Send(email));
        }
    }
}
