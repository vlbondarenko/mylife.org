using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using serverapp.Models;
using serverapp.Infrastructure;
using System.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using serverapp.Controllers;
using Microsoft.AspNetCore.Http;
using System.Text.Encodings.Web;

namespace serverapp.Services
{ 
    public class AccountService:IAccountService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IJWTGenerator _jWTGenerator;
        private readonly AppIdentityDbContext _userDbContext;

        public AccountService(UserManager<AppUser> userManager,SignInManager<AppUser> signInManager,IJWTGenerator jWTGenerator, AppIdentityDbContext userDbContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jWTGenerator = jWTGenerator;
            _userDbContext = userDbContext;
      
        }

        public async Task<UserModel> Login(LoginModel loginModel)
        {
            var user = await _userManager.FindByEmailAsync(loginModel.Email);

            if (user == null)
            {
                throw new RestExcteption(HttpStatusCode.Unauthorized, new {  Error = "Invalid email address or password" });
            }

            var loginResult = await _signInManager.CheckPasswordSignInAsync(user, loginModel.Password, false);

            if (loginResult.Succeeded)
                return new UserModel() 
                { 
                    DisplayName = user.UserName,
                    Id = user.Id,
                    Token = _jWTGenerator.CreateToken(user) 
                };
                   
    
            throw new RestExcteption(HttpStatusCode.Unauthorized, new { Error = "Invalid email address or password" });
        }

        public async Task<UserModel> Register(RegisterModel registerModel)
        {
       
            if (await _userDbContext.Users.Where(x => x.Email == registerModel.Email).AnyAsync())
                throw new RestExcteption(HttpStatusCode.BadRequest, new { Error = "Email is already exist" });

            var newUser = new AppUser()
            {
                Email = registerModel.Email,
                UserName = registerModel.FirstName + " " + registerModel.LastName
            };

            var registerResult = await _userManager.CreateAsync(newUser, registerModel.Password);

            if (registerResult.Succeeded)
                return new UserModel
                {
                    DisplayName = newUser.UserName,
                    Id = newUser.Id,
                    Token = _jWTGenerator.CreateToken(newUser)
                };

            throw new RestExcteption(HttpStatusCode.BadRequest,new { Error = "Client create failure"});
        }

       public async Task<UserModel> GetById(string id)
       {
            var user = await _userManager.FindByEmailAsync(id);

            if (user == null)
            {
                throw new RestExcteption(HttpStatusCode.NotFound, new { Error = "User not found" });
            }

            return new UserModel
            {
                DisplayName=user.UserName,
                Id = user.Id
            };
       }

        public async Task<dynamic> SendConfirmEmailMessage(AppUser newUser, string originUrl)
        {
            if (newUser == null)
            {
                throw new RestExcteption(HttpStatusCode.NotFound, new { Message = "User not found" });
            }

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);
            var confirmEmailLink = $"{originUrl}/account/send-confirm-email?id={newUser.Id}?token={token}";

            return new { Confirmlink = confirmEmailLink };
        }

        public async Task ConfirmEmail (string id,string token)
        {
            var user =await _userManager.FindByIdAsync(id);

            if (user == null)
                throw new RestExcteption(HttpStatusCode.BadRequest, new { MEssage = "User not found" });

            var resultOfConfirm = await _userManager.ConfirmEmailAsync(user, token);

            if (resultOfConfirm.Succeeded)
                return;

            throw new RestExcteption(HttpStatusCode.BadRequest, new { Message = "Email not confirm" });
        }
    }
}
