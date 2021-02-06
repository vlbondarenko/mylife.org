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
    public enum MessageContext
    {
        EmailAdressConfirmation,
        ResetPassword
    }

    public class MessageSendingService:IMessageSendingService
    {
        private readonly MessageOptions _messageOptions;

        public MessageSendingService(IOptionsSnapshot<MessageOptions> options)
        {
            _messageOptions = options.Value;
        }

        public void SendMessage(string to, string content, MessageContext context)
        {
            var html = "";
            var subject = "";
            switch (context)
            {
                case MessageContext.EmailAdressConfirmation:
                    {
                        subject = "Sign-up Verification API - Verify Email";
                        html = $@"<p>Please click the below link to verify your email address:</p>
                               <p><a href=""{content}"">{content}</a></p>";
                        Send(to, subject, html, _messageOptions.MessageFrom);
                        break;
                    }
                case MessageContext.ResetPassword:
                    {
                        subject = "Password Recovery";
                        html = $@"<p>Please click the below link to verify your email address and recowery password:</p>
                               <p><a href=""{content}"">{content}</a></p>";
                        Send(to, subject, html, _messageOptions.MessageFrom);
                        break;
                    }
            }
            return;
        }

        public void Send(string to, string subject, string html, string from = null)
        {
            // create message
            var message = new MimeMessage();
            message.From.Add(MailboxAddress.Parse(from ?? _messageOptions.MessageFrom));
            message.To.Add(MailboxAddress.Parse(to));
            message.Subject = subject;
            message.Body = new TextPart(TextFormat.Html) { Text = html };

            // send message
            using var smtp = new SmtpClient();
            smtp.Connect(_messageOptions.SmtpHost, _messageOptions.SmtpPort, SecureSocketOptions.StartTls);
            smtp.Authenticate(_messageOptions.SmtpUser, _messageOptions.SmtpPass);
            smtp.Send(message);
            smtp.Disconnect(true);
        }
    }
}
