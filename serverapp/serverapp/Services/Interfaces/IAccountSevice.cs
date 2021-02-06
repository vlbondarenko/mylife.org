using System.Threading.Tasks;
using serverapp.Models;
using Microsoft.AspNetCore.Http;

namespace serverapp.Services
{
   
   public interface IAccountService
   {
        Task<UserData> SignInAsync(SignInData signInData);

        Task<AppUser> SignUpAsync(SignUpData signUpData);
        Task<string> GenerateEmailConfirmationTokenAsync(AppUser user);
        Task SendEmailAdressConfirmationMassageAsync(string emailAdress, string confirmationLink);
        Task ConfirmEmailAdressAsync(string id, string token);

        Task ResetPasswordAsync(string email, HttpContext context);
        Task SendResetPasswordMessageAsync(string email, HttpRequest request);
        Task ConfirmResetPasswordAsync(string id, string token, string newPassword, HttpResponse response);


        Task<AppUser> GetUserByEmailAsync(string email);
    }

  
}
