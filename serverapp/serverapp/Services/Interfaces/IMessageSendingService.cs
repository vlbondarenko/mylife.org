using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace serverapp.Services
{
    public interface IMessageSendingService
    {
        void SendMessage(string to, string content, MessageContext context);
    }
}
