using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using serverapp.Infrastructure;
using MimeKit;
using MimeKit.Text;
using MailKit.Net.Smtp;
using MailKit.Security;

namespace serverapp.Services
{
    public enum EmailContext
    {
        ConfirmationEmailAdress,
        PasswordRecowery
    }

    public class EmailSendingService:IEmailSendingService
    {
        private readonly EmailOptions _emailOptions;

        public EmailSendingService(IOptionsSnapshot<EmailOptions> options)
        {
            _emailOptions = options.Value;
        }

        public void SendEmail(string to, string content, EmailContext context)
        {
            var html = "";
            var subject = "";
            switch (context)
            {
                case EmailContext.ConfirmationEmailAdress:
                    {
                        subject = "Sign-up Verification API - Verify Email";
                        html = $@"<p>Please click the below link to verify your email address:</p>
                               <p><a href=""{content}"">{content}</a></p>";
                        Send(to, subject, html, _emailOptions.EmailFrom);
                        break;
                    }
                case EmailContext.PasswordRecowery:
                    {
                        subject = "Password Recovery";
                        html = $@"<p>Please click the below link to verify your email address and recowery password:</p>
                               <p><a href=""{content}"">{content}</a></p>";
                        Send(to, subject, html, _emailOptions.EmailFrom);
                        break;
                    }
            }
            return;
        }

        public void Send(string to, string subject, string html, string from = null)
        {
            // create message
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(from ?? _emailOptions.EmailFrom));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Html) { Text = html };

            // send email
            using var smtp = new SmtpClient();
            smtp.Connect(_emailOptions.SmtpHost, _emailOptions.SmtpPort, SecureSocketOptions.StartTls);
            smtp.Authenticate(_emailOptions.SmtpUser, _emailOptions.SmtpPass);
            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}
