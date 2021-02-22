using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using serverapp.Models;
using serverapp.Infrastructure;
using System.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace serverapp.Services
{

    public static class ErrorMessages
    {
        public static readonly string EmailAlreadyExist = "The user with this email already exist";
        public static readonly string UserNotFound = "Such the user does not exist";
        public static readonly string EmailNotConfirm = "The link to confirm the email address is not valid. Try again";
        public static readonly string NotRegistered = "The user is not registered";
        public static readonly string PasswordNotReset = "Password could not be reset";
        public static readonly string SendingMessageFailed = "Failed to send a message with a link to confirm the email address. Log in and try sending the message again through the settings.";
    }

    public class AccountService : IAccountService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IJWTGenerator _jWTGenerator;
        private readonly AppIdentityDbContext _userDbContext;
        private readonly IMessageSendingService _messageSendingService;
        private readonly ILogger<AccountService> _logger;


        public AccountService(  UserManager<AppUser> userManager, 
                                SignInManager<AppUser> signInManager, 
                                IJWTGenerator jWTGenerator, 
                                AppIdentityDbContext userDbContext, 
                                IMessageSendingService messageSendingService,
                                ILogger<AccountService> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jWTGenerator = jWTGenerator;
            _userDbContext = userDbContext;
            _messageSendingService = messageSendingService;
            _logger = logger;
        }

        #region SignIn functionality

        public async Task<UserData> SignInAsync(SignInData signInData)
        {
            var user = await _userManager.FindByEmailAsync(signInData.Email);
            if (user == null)
                throw new RestExcteption(HttpStatusCode.BadRequest, new { Message = "Invalid email address or password" });

            var loginResult = await _signInManager.CheckPasswordSignInAsync(user, signInData.Password, false);
            if (loginResult.Succeeded)
                return new UserData   
                {
                    DisplayName = user.UserName,
                    Id = user.Id,
                    Token = _jWTGenerator.CreateToken(user)
                };

            throw new RestExcteption(HttpStatusCode.BadRequest, new { Message = "Invalid email address or password" });
        }

        #endregion


        #region SignUp functionality

        public async Task SignUpAsync(SignUpData signUpData)
        {
            if (await _userDbContext.Users.Where(x => x.Email == signUpData.UserEmail).AnyAsync())
                throw new RestExcteption(HttpStatusCode.BadRequest, new { Message = ErrorMessages.EmailAlreadyExist });

            var newUser = new AppUser()
            {
                Email = signUpData.UserEmail,
                UserName = signUpData.FirstName,
                LastName = signUpData.LastName
            };

            var signUpResult = await _userManager.CreateAsync(newUser, signUpData.Password);
            
            if (signUpResult.Succeeded)
            {
                await SendEmailAdressConfirmationMassageAsync(newUser.Email);
                return;
            }     
            else                              
                throw new RestExcteption(HttpStatusCode.InternalServerError, new { Message = ErrorMessages.NotRegistered });
        }


        public async Task SendEmailAdressConfirmationMassageAsync(string emailAdress)
        {
            var user = await _userManager.FindByEmailAsync(emailAdress);
            if (user == null)
                throw new RestExcteption(HttpStatusCode.BadRequest, new { Message = ErrorMessages.UserNotFound});

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var confirmationLink = $"https://localhost:5001/api/account?id={user.Id}&token={token}";


            //Just catch the exceptions that occurred when sending the message. 
            //The user will be notified on the client that the message may not have been sent, 
            //so that he can repeat the message later through the profile settings
            try
            {
                _messageSendingService.SendMessage(emailAdress, confirmationLink, MessageContext.EmailAdressConfirmation);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
            }   
        }


        public async Task ConfirmEmailAdressAsync(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                throw new RestExcteption(HttpStatusCode.BadRequest, new { Message = ErrorMessages.UserNotFound });

            var resultOfConfirm = await _userManager.ConfirmEmailAsync(user, token);

            if (resultOfConfirm.Succeeded)               
                return;
            else
                throw new RestExcteption(HttpStatusCode.BadRequest, new { Message = ErrorMessages.EmailNotConfirm });
        }

        #endregion


        #region Reset Password functionality

        
        public async Task SendResetPasswordMessageAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                throw new RestExcteption(HttpStatusCode.BadRequest, new { Message = ErrorMessages.UserNotFound });
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var confirmationLink = $"https://localhost:5001/api/account/verify-token?id={user.Id}&token={token}";

            try
            {
                _messageSendingService.SendMessage(user.Email, confirmationLink, MessageContext.ResetPassword);
            }
            catch
            {
                throw new RestExcteption(HttpStatusCode.InternalServerError, new { Message = ErrorMessages.SendingMessageFailed });
            }  
        }


        public async Task<bool> VerifyResetPasswordTokenAsync(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                throw new RestExcteption(HttpStatusCode.BadRequest, new { Message = ErrorMessages.UserNotFound });

            var tokenProvider = _userManager.Options.Tokens.PasswordResetTokenProvider;
            var purpose = "ResetPassword";
            var result = await _userManager.VerifyUserTokenAsync(user, tokenProvider, purpose, token);

            return result ? result : throw new RestExcteption(HttpStatusCode.BadRequest, new { Message = ErrorMessages.EmailNotConfirm});
        }


        public async Task ResetPasswordAsync(string userId, string token, string newPassword)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                throw new RestExcteption(HttpStatusCode.BadRequest, new { Message = ErrorMessages.UserNotFound });

            var resultOfReset = await _userManager.ResetPasswordAsync(user, token, newPassword);

            if (resultOfReset.Succeeded)
            {
                return;
            }

            throw new RestExcteption(HttpStatusCode.BadRequest, new { Message = ErrorMessages.PasswordNotReset });
        }


        #endregion


        public async Task<AppUser> GetUserByEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                throw new RestExcteption(HttpStatusCode.NoContent, new { Message = ErrorMessages.UserNotFound });

            return user;           
        }

        public async Task<bool> CheckUniquenessOfEmailAsync(string email)
        {
            var user =await _userManager.FindByEmailAsync(email); 

            return user == null ? true : false;
        }
    }
}
