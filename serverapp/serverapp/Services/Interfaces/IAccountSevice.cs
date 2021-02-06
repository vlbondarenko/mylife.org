using System.Threading.Tasks;
using serverapp.Models;
using Microsoft.AspNetCore.Http;

namespace serverapp.Services
{
   
   public interface IAccountService
   {
        Task<UserData> SignInAsync(SignInData signInData);

        Task SignUpAsync(SignUpData signUpData);
        Task SendEmailAdressConfirmationMassageAsync(string email);
        Task ConfirmEmailAdressAsync(string id, string token);

        Task SendResetPasswordMessageAsync(string email);
        Task<bool> VerifyResetPasswordTokenAsync(string id, string token);
        Task ResetPasswordAsync(string id, string token, string newPassword);


        Task<AppUser> GetUserByEmailAsync(string email);
    }

  
}
