using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using serverapp.Models;
using serverapp.Infrastructure;
using System.Net;
using Microsoft.EntityFrameworkCore;

namespace serverapp.Services
{
    public class UserData
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
        public string Token { get; set; }

        public UserData()
        {
            Name = "";
            Email = "";
            Message = "";
            Token = "";
        }
    }

    public class LoginData
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class RegisterData
    {
        public string Email { get; set; }
        public string Nickname { get; set; }
        public string Password { get; set; }
    }

    
    public class UserService:IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IJWTGenerator _jWTGenerator;
        private readonly AppIdentityDbContext _userDbContext;

        public UserService(UserManager<AppUser> userManager,SignInManager<AppUser> signInManager,IJWTGenerator jWTGenerator, AppIdentityDbContext userDbContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jWTGenerator = jWTGenerator;
            _userDbContext = userDbContext;
        }

        public async Task<UserData> Login(LoginData loginData)
        {
            var user = await _userManager.FindByEmailAsync(loginData.Email);

            if (user == null)
            {
                throw new RestExcteption(HttpStatusCode.Unauthorized);
            }

            var loginResult = await _signInManager.CheckPasswordSignInAsync(user, loginData.Password, false);

            if (loginResult.Succeeded)
                return new UserData() 
                { 
                    Email = user.Email, 
                    Message = "", 
                    Name = user.UserName, 
                    Token = _jWTGenerator.CreateToken(user) 
                };
                   

            throw new RestExcteption(HttpStatusCode.Unauthorized);
        }

        public async Task<UserData> Register(RegisterData registerData)
        {
            if (await _userDbContext.Users.Where(x => x.Email == registerData.Email).AnyAsync())
                throw new RestExcteption(HttpStatusCode.BadRequest, new { Email = "Email is already exist" });

            if (await _userDbContext.Users.Where(x => x.Nickname == registerData.Nickname).AnyAsync())
                throw new RestExcteption(HttpStatusCode.BadRequest, new { Nickname = "Nickname is already exist" });

            var user = new AppUser()
            {
                Email = registerData.Email,
                Nickname = registerData.Nickname,
                UserName = registerData.Nickname
            };

            var registerResult = await _userManager.CreateAsync(user, registerData.Password);

            if (registerResult.Succeeded)
                return new UserData
                {
                    Email = user.Email,
                    Name = user.UserName,
                    Message = "",
                    Token = _jWTGenerator.CreateToken(user)
                };

            throw new RestExcteption(HttpStatusCode.BadRequest,new { Message = "Client create failure"});
        }
    }
}
