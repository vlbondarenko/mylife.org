using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using MailKit.Net.Smtp;
using MailKit.Security;


using Infrastructure.Interfaces;

namespace Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailOptions _emailOptions;

        public EmailService(IOptionsSnapshot<EmailOptions> emailOptions)
        {
            _emailOptions = emailOptions.Value;
        }
        public void SendEmail(string to, string subject, string content, string from = null)
        {
            // create message
            var message = new MimeMessage();
            message.From.Add(MailboxAddress.Parse(from ?? _emailOptions.EmailFrom));
            message.To.Add(MailboxAddress.Parse(to));
            message.Subject = subject;
            message.Body = new TextPart(TextFormat.Html) { Text = content };

            // send message
            using var smtp = new SmtpClient();
            smtp.Connect(_emailOptions.SmtpHost, _emailOptions.SmtpPort, SecureSocketOptions.StartTls);
            smtp.Authenticate(_emailOptions.SmtpUser, _emailOptions.SmtpPass);
            smtp.Send(message);
            smtp.Disconnect(true);
        }
    }
}
