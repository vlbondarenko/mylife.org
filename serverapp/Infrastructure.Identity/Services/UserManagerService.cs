using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http;
using Infrastructure.Identity.Exceptions;
using Infrastructure.Identity.Interfaces;
using Infrastructure.Identity.Data;

namespace Infrastructure.Identity.Services
{
    internal class UserManagerService:IUserManagerService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IEmailService _emailService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        
        public UserManagerService (UserManager<AppUser> userManager, IEmailService emailService, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _emailService = emailService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task SendConfirmationEmail (string userEmail)
        {
            var user = await _userManager.FindByEmailAsync(userEmail);
            if (user is null)
                throw new UserNotFoundException($"User {userEmail} not found");

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var confirmationLink = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}/api/account/confirm-email?id={user.Id}&token={token}";


            //Just catch the exceptions that occurred when sending the message. 
            //The user will be notified on the client that the message may not have been sent, 
            //so that he can repeat the message later through the profile settings
            try
            {
               _emailService.SendEmail(userEmail, "Confirm Email", confirmationLink);
            }
            catch (Exception e)
            {
                //TODO: add logging
            }
        }
    }
}
