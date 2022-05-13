using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EmailService.Model;

namespace EmailService
{
    public interface IEmailSender
    {
        void SendEmail(Message message);
        Task SendEmailAsync(Message message);
    }
}
