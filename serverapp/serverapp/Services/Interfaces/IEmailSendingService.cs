using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace serverapp.Services
{
    public interface IEmailSendingService
    {
        void SendEmail(string to, string content, EmailContext context);
    }
}
