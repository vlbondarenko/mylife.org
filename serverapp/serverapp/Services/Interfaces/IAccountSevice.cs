using System.Threading.Tasks;
using serverapp.Models;
using Microsoft.AspNetCore.Mvc;

namespace serverapp.Services
{
   
   public interface IAccountService
   {
        Task<UserModel> Login(LoginModel loginData);
        Task<UserModel> Register(RegisterModel registerData);
        Task<UserModel> GetById(string id);
        Task<dynamic> SendConfirmEmailMessage(AppUser user, string origin);
        Task ConfirmEmail(string id, string token);
   }

  
}
