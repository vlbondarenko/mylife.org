using System;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
    public interface IEmailService
    {
        Task SendEmail(string email, string subject, string message);
    }
}
