using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using serverapp.Models;
using serverapp.Infrastructure;
using System.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace serverapp.Services
{ 
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

        public async Task<UserModel> Login(LoginModel loginData)
        {
            var user = await _userManager.FindByEmailAsync(loginData.Email);

            if (user == null)
            {
                throw new RestExcteption(HttpStatusCode.Unauthorized);
            }

            var loginResult = await _signInManager.CheckPasswordSignInAsync(user, loginData.Password, false);

            if (loginResult.Succeeded)
                return new UserModel() 
                { 
                    Email = user.Email, 
                    Errors = null, 
                    Name = user.UserName, 
                    Token = _jWTGenerator.CreateToken(user) 
                };
                   
    
            throw new RestExcteption(HttpStatusCode.Unauthorized);
        }

        public async Task<UserModel> Register(RegisterModel registerData)
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
                return new UserModel
                {
                    Email = user.Email,
                    Name = user.UserName,
                    Errors = user.Id,
                    Token = _jWTGenerator.CreateToken(user)
                };

            throw new RestExcteption(HttpStatusCode.BadRequest,new { Message = "Client create failure"});
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
                Name = user.Nickname,
                Email = user.Email,
                Errors = null,
                Token = "Fack you"
            };
        }
    }
}
