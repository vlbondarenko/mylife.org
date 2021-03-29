using System;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
    public interface IEmailService
    {
        void SendEmail(string to, string subject, string content, string from = null);
    }
}
