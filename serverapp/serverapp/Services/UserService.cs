using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using serverapp.Models;
using serverapp.Infrastructure;
using System.Net;
using Microsoft.EntityFrameworkCore;

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

        public async Task<UserModel> Login(LoginModel loginModel)
        {
            var user = await _userManager.FindByEmailAsync(loginModel.Email);

            if (user == null)
            {
                throw new RestExcteption(HttpStatusCode.Unauthorized);
            }

            var loginResult = await _signInManager.CheckPasswordSignInAsync(user, loginModel.Password, false);

            if (loginResult.Succeeded)
                return new UserModel() 
                { 
                    DisplayName = user.Email,
                    Id = user.Id,
                    Token = _jWTGenerator.CreateToken(user) 
                };
                   
    
            throw new RestExcteption(HttpStatusCode.Unauthorized);
        }

        public async Task<UserModel> Register(RegisterModel registerModel)
        {
       
            if (await _userDbContext.Users.Where(x => x.Email == registerModel.Email).AnyAsync())
                throw new RestExcteption(HttpStatusCode.BadRequest, new { Error = "Email is already exist" });

            var user = new AppUser()
            {
                Email = registerModel.Email,
                UserName = registerModel.FirstName + registerModel.LastName
            };

            var registerResult = await _userManager.CreateAsync(user, registerModel.Password);

            if (registerResult.Succeeded)
                return new UserModel
                {
                    DisplayName = user.UserName,
                    Id = user.Id,
                    Token = _jWTGenerator.CreateToken(user)
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
    }
}
