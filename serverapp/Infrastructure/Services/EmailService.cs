using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Infrastructure.Interfaces;

namespace Infrastructure.Services
{
    public class EmailService:IEmailService
    {
       public async Task SendEmail(string email, string subject, string message)
        {

        }
    }
}
