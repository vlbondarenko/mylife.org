using System.Threading.Tasks;
using serverapp.Models;
using Microsoft.AspNetCore.Http;

namespace serverapp.Services
{
   
   public interface IAccountService
   {
        Task<UserData> SignInAsync(SignInData signInData);
        Task SignUpAsync(SignUpData signUpData, HttpContext context);
        Task RestorePassword(string email, HttpContext context);
        Task SendEmailConfirmationMessageAsync(AppUser user, string originUrl, EmailContext context);
        Task ConfirmEmailAdressAsync(string id, string token, HttpContext context);
        Task<UserData> GetById(string id);
    }

  
}
