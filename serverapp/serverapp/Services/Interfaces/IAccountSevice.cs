using System.Threading.Tasks;
using serverapp.Models;
using Microsoft.AspNetCore.Http;

namespace serverapp.Services
{
   
   public interface IAccountService
   {
        Task<UserData> SignInAsync(SignInData signInData);

        Task SignUpAsync(SignUpData signUpData, HttpContext context);
        Task SendEmailAdressConfirmationMessageAsync(string email, HttpRequest httpContext);
        Task ConfirmEmailAdressAsync(string id, string token, HttpResponse response);

        Task ResetPasswordAsync(string email, HttpContext context);
        Task SendResetPasswordMessageAsync(string email, HttpRequest request);
        Task ConfirmResetPasswordAsync(string id, string token, string newPassword, HttpResponse response);


        Task<UserData> GetById(string id);
    }

  
}
