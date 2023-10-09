using Application.DTOs;
using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class EmailService : IEmailService
    {
        public async Task<bool> SendEmail(EmailDTO emailDTO)
        {
            var senderEmail = new MailAddress("mehreganabdiwebmail@gmail.com");
            var receiverEmail = new MailAddress(emailDTO.reciever, "Receiver");
            var password = "kxbo ipin pkyn vgfo";
            var subject = emailDTO.subject;
            var body = emailDTO.message + $"\n{DateTime.Now}\nThanks For Choosing Us, Good Luck .";
            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(senderEmail.Address, password)
            };
            using (var mess = new MailMessage(senderEmail, receiverEmail)
            {
                Subject = subject,
                Body = body
            })
            {
                try
                {
                    smtp.Send(mess);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
    }
}
