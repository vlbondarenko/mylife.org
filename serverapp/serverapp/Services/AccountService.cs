using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using serverapp.Models;
using serverapp.Infrastructure;
using System.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace serverapp.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IJWTGenerator _jWTGenerator;
        private readonly AppIdentityDbContext _userDbContext;
        private readonly IMessageSendingService _messageSendingService;


        public AccountService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IJWTGenerator jWTGenerator, AppIdentityDbContext userDbContext, IMessageSendingService messageSendingService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jWTGenerator = jWTGenerator;
            _userDbContext = userDbContext;
            _messageSendingService = messageSendingService;
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

        public async Task<AppUser> SignUpAsync(SignUpData signUpData)
        {
            if (await _userDbContext.Users.Where(x => x.Email == signUpData.Email).AnyAsync())
                throw new RestExcteption(HttpStatusCode.BadRequest, new { Message = "Email is already exist" });

            var newUser = new AppUser()
            {
                Email = signUpData.Email,
                UserName = signUpData.FirstName,
                LastName = signUpData.LastName
            };

            var signUpResult = await _userManager.CreateAsync(newUser, signUpData.Password);
            
            if (signUpResult.Succeeded)      
                return newUser; 
            else                              
                throw new RestExcteption(HttpStatusCode.BadRequest, new { Message = "Client create failure" });
        }


        public async Task<string> GenerateEmailConfirmationTokenAsync(AppUser user)
        {
            if (user == null)
            {
                throw new RestExcteption(HttpStatusCode.BadRequest, new { Message = "User not found" });
            }

            return await _userManager.GenerateEmailConfirmationTokenAsync(user);
        }


        public async Task SendEmailAdressConfirmationMassageAsync(string emailAdress, string confirmationLink)
        {
            if (!await _userDbContext.Users.Where(x => x.Email == emailAdress).AnyAsync())
                throw new RestExcteption(HttpStatusCode.BadRequest, new { Message = "The user with this email does not exist" });
            
            _messageSendingService.SendMessage(emailAdress, confirmationLink, MessageContext.EmailAdressConfirmation);
        }


        public async Task ConfirmEmailAdressAsync(string id, string token)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
                throw new RestExcteption(HttpStatusCode.BadRequest, new { Message = "User not found" });

            var resultOfConfirm = await _userManager.ConfirmEmailAsync(user, token);

            if (resultOfConfirm.Succeeded)               
                return;
            else
                throw new RestExcteption(HttpStatusCode.BadRequest, new { Message = "Email not confirm" });
        }


        private void RedirectClientToLocation(HttpResponse response, string location)
        {
            response.StatusCode = (int)HttpStatusCode.Moved;
            response.Headers["location"] = location;
        }

        #endregion


        #region Reset Password functionality

        public async Task ResetPasswordAsync(string email, HttpContext context)
        {
            var user =await _userManager.FindByEmailAsync(email);
            if (user == null)
                throw new RestExcteption(HttpStatusCode.BadRequest, new { Message = "User not found" });

            await SendResetPasswordMessageAsync(user.Email, context.Request);
            return;
        }


        public async Task SendResetPasswordMessageAsync(string email, HttpRequest request)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                throw new RestExcteption(HttpStatusCode.BadRequest, new { Message = "User not found" });
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var emailConfirmationLink = $"{request.Scheme}://{request.Host}/api/account/confirm-email?id={user.Id}&token={token}";

            _messageSendingService.SendMessage(user.Email, emailConfirmationLink, MessageContext.EmailAdressConfirmation);
        }

        public async Task ConfirmResetPasswordAsync(string id, string token, string newPassword, HttpResponse response)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
                throw new RestExcteption(HttpStatusCode.BadRequest, new { Message = "User not found" });

            var resultOfConfirm = await _userManager.ResetPasswordAsync(user, token, newPassword);

            if (resultOfConfirm.Succeeded)
            {
                RedirectClientToLocation(response, "http://localhost:8080/reset-password-succes");
                return;
            }

            throw new RestExcteption(HttpStatusCode.BadRequest, new { Message = "Email not confirm" });
        }

        #endregion


        public async Task<AppUser> GetUserByEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                throw new RestExcteption(HttpStatusCode.NoContent, new { Message = "User not found" });

            return user;           
        }
    }
}
