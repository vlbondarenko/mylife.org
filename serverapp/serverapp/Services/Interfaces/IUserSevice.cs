using System.Threading.Tasks;
using serverapp.Models;
using Microsoft.AspNetCore.Mvc;

namespace serverapp.Services
{
   
   public interface IUserService
   {
        Task<UserModel> Login(LoginModel loginData);
        Task<UserModel> Register(RegisterModel registerData);
        Task<UserModel> GetById(string id);
   }

  
}
