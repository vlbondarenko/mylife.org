using System.Threading.Tasks;
using serverapp.Models;
using Microsoft.AspNetCore.Http;

namespace serverapp.Services
{
   
   public interface IAccountService
   {
        Task<UserData> SignInAsync(SignInData signInData);
        Task<UserData> SignUpAsync(SignUpData signUpData);
        Task<UserData> GetById(string id);
        Task<dynamic> SendEmailAdressConfirmationMessageAsync(string userId, string originUrl);
        Task ConfirmEmailAdressAsync(string id, string token,HttpContext context);
   }

  
}
