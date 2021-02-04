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


        public async Task<UserData> SignUpAsync(SignUpData signUpData)
        {
            if (await _userDbContext.Users.Where(x => x.Email == signUpData.Email).AnyAsync())
                throw new RestExcteption(HttpStatusCode.BadRequest, new { Message = "Email is already exist" });

            var newUser = new AppUser()
            {
                Email = signUpData.Email,
                UserName = signUpData.FirstName + " " + signUpData.LastName
            };

            var registerResult = await _userManager.CreateAsync(newUser, signUpData.Password);
            if (registerResult.Succeeded)
                return new UserData
                {
                    DisplayName = newUser.UserName,
                    Id = newUser.Id,
                    Token = _jWTGenerator.CreateToken(newUser)
                };
               
            throw new RestExcteption(HttpStatusCode.BadRequest, new { Message = "Client create failure" });
        }


        public async Task<dynamic> SendEmailAdressConfirmationMessageAsync(string userId, string originUrl)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new RestExcteption(HttpStatusCode.BadRequest, new { Message = "User not found" });
            }

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var emailConfirmationLink = $"{originUrl}/account/send-confirm-email?id={user.Id}?token={token}";

            _emailSendingService.SendEmail(user.Email ,emailConfirmationLink, EmailContext.ConfirmationEmailAdress);

            return new { Confirmlink = emailConfirmationLink };
        }


        public async Task ConfirmEmailAdressAsync(string id, string token,HttpContext context)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
                throw new RestExcteption(HttpStatusCode.BadRequest, new { Message = "User not found" });

            var resultOfConfirm = await _userManager.ConfirmEmailAsync(user, token);

            if (resultOfConfirm.Succeeded)
            {
                context.Response.StatusCode = (int)HttpStatusCode.OK;
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
