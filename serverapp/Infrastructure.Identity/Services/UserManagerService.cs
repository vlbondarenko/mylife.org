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
            var confirmationLink = GetLink("confirm-email", user.Id, token);


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


        public async Task SendResetPasswordEmail(string userEmail)
        {
            var user = await _userManager.FindByEmailAsync(userEmail);
            if (user is null)
                throw new UserNotFoundException($"User {userEmail} not found");

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var resetPasswordLink = GetLink("reset-password", user.Id, token);

            try
            {
                _emailService.SendEmail(userEmail, "Reset Password", resetPasswordLink);
            }
            catch (Exception e)
            {
                //TODO: add logging
            }

        }

        public async Task<bool> VerifyResetPasswordTokenAsync(string userId,string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                throw new UserNotFoundException($"User id{userId} not found");

            var tokenProvider = _userManager.Options.Tokens.PasswordResetTokenProvider;
            var purpose = "ResetPassword";
            var result = await _userManager.VerifyUserTokenAsync(user, tokenProvider, purpose, token);

            return result ? result : throw new IdentityException("Reset password failure");
        }


        public async Task ResetPasswordAsync(string userId, string token, string newPassword)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                throw new UserNotFoundException($"User id{userId} not found");

            var resultOfReset = await _userManager.ResetPasswordAsync(user, token, newPassword);

            if (resultOfReset.Succeeded)
            {
                return;
            }

            throw new IdentityException("Reset password failure");
        }

        private string GetLink(string method, string id, string token)
        {
            return $"<a href=\"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}/api/account/{method}?id={id}&token={token}\">Click Here</a>";

        }
    }
}
