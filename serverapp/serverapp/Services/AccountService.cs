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
        private readonly IEmailSendingService _emailSendingService;


        public AccountService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IJWTGenerator jWTGenerator, AppIdentityDbContext userDbContext, IEmailSendingService emailSendingService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jWTGenerator = jWTGenerator;
            _userDbContext = userDbContext;
            _emailSendingService = emailSendingService;
        }


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


        public async Task SignUpAsync(SignUpData signUpData, HttpContext context)
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
            {
                var emailConfirmationApiPath = $"{context.Request.Scheme}://{context.Request.Host}/api/account/confirm-email";
                await SendEmailConfirmationMessageAsync(newUser, emailConfirmationApiPath, EmailContext.ConfirmationEmailAdress);

                return;
            }
                              
            throw new RestExcteption(HttpStatusCode.BadRequest, new { Message = "Client create failure" });
        }


        public async Task RestorePassword(string email, HttpContext context)
        {
            var user =await _userManager.FindByEmailAsync(email);
            if (user == null)
                throw new RestExcteption(HttpStatusCode.BadRequest, new { Message = "User not found" });

            var origiuUrl = context.Request.Headers["origin"];
            await SendEmailConfirmationMessageAsync(user, origiuUrl, EmailContext.PasswordRecowery);
        }


        public async Task SendEmailConfirmationMessageAsync(AppUser user, string emailConfirmationApiPath, EmailContext context)
        {
            if (user == null)
            {
                throw new RestExcteption(HttpStatusCode.BadRequest, new { Message = "User not found" });
            }

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var emailConfirmationLink = $"{emailConfirmationApiPath}?id={user.Id}&token={token}";

            _emailSendingService.SendEmail(user.Email ,emailConfirmationLink, context);
        }


        public async Task ConfirmEmailAdressAsync(string id, string token, HttpContext context)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
                throw new RestExcteption(HttpStatusCode.BadRequest, new { Message = "User not found" });

            var resultOfConfirm = await _userManager.ConfirmEmailAsync(user, token);

            if (resultOfConfirm.Succeeded)
            {
                context.Response.StatusCode = (int)HttpStatusCode.Moved;
                context.Response.Headers["location"] ="http://localhost:8080/confirmemail";
                return;
            }
               
            throw new RestExcteption(HttpStatusCode.BadRequest, new { Message = "Email not confirm" });
        }

        public async Task<UserData> GetById(string id)
        {
            var user = await _userManager.FindByEmailAsync(id);
            if (user == null)
                throw new RestExcteption(HttpStatusCode.NoContent, new { Message = "User not found" });

            return new UserData
            {
                DisplayName = user.UserName,
                Id = user.Id
            };
        }
    }
}
