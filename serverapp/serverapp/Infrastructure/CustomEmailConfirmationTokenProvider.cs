using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using serverapp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Text;


namespace serverapp.Infrastructure
{
    public class CustomEmailConfirmationTokenProvider<TUser> : DataProtectorTokenProvider<TUser> where TUser:class
    {
        public CustomEmailConfirmationTokenProvider(IDataProtectionProvider dataProtectionProvider, 
                                                    IOptions<EmailConfirmationTokenProviderOptions> options, 
                                                    ILogger<DataProtectorTokenProvider<TUser>> logger) : base(dataProtectionProvider, options, logger) { }


        public override async Task<string> GenerateAsync(string purpose, UserManager<TUser> manager, TUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            var ms = new MemoryStream();
            var userId = await manager.GetUserIdAsync(user);
            using (var writer = new BinaryWriter(ms, new UTF8Encoding(true, false), true))
            {
                writer.Write(DateTimeOffset.UtcNow.UtcTicks);
                writer.Write(userId);
                writer.Write(purpose ?? "");
                string stamp = null;
                if (manager.SupportsUserSecurityStamp)
                {
                    stamp = await manager.GetSecurityStampAsync(user);
                }
                writer.Write(stamp ?? "");
            }
            var protectedBytes = Protector.Protect(ms.ToArray());
            //return Convert.ToBase64String(protectedBytes);
            return "pidor";
        }
    }

    public class EmailConfirmationTokenProviderOptions : DataProtectionTokenProviderOptions
    {
        public EmailConfirmationTokenProviderOptions()
        {
            Name = "EmailDataProtectorTokenProvider";
            TokenLifespan = TimeSpan.FromHours(4);
        }
    }
}
