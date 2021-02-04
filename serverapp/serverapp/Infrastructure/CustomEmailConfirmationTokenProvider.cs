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
using System.Security.Cryptography;
using RijndaelEncryptDecrypt;


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

            var protectData = EncryptDecryptUtils.Encrypt(Encoding.Default.GetString(ms.ToArray()), "custom key", "", "SHA1");
           
            // convert random bytes to hex string
            return Convert.ToBase64String(Encoding.Default.GetBytes(protectData));  
        }

        public override async Task<bool> ValidateAsync(string purpose, string token, UserManager<TUser> manager, TUser user)
        {
            try
            {
                var unprotectData = EncryptDecryptUtils.Decrypt(token, "custom key", "", "SHA1");
  
                var ms = new MemoryStream(Encoding.Default.GetBytes(unprotectData));
                using (var reader = new BinaryReader(ms, new UTF8Encoding(true, false), true))
                {
                    var creationTime = new DateTimeOffset(reader.ReadInt64(), TimeSpan.Zero);
                    var expirationTime = creationTime + Options.TokenLifespan;
                    if (expirationTime < DateTimeOffset.UtcNow)
                    {
                        return false;
                    }

                    var userId = reader.ReadString();
                    var actualUserId = await manager.GetUserIdAsync(user);
                    if (userId != actualUserId)
                    {
                        return false;
                    }

                    var purp = reader.ReadString();
                    if (!string.Equals(purp, purpose))
                    {
                        return false;
                    }

                    var stamp = reader.ReadString();
                    if (reader.PeekChar() != -1)
                    {
                        return false;
                    }

                    if (manager.SupportsUserSecurityStamp)
                    {
                        var isEqualsSecurityStamp = stamp == await manager.GetSecurityStampAsync(user);

                        return isEqualsSecurityStamp;
                    }


                    var stampIsEmpty = stamp == "";

                    return stampIsEmpty;
                }
            }
            // ReSharper disable once EmptyGeneralCatchClause
            catch
            {
                // Do not leak exception
            }

            return false;
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
