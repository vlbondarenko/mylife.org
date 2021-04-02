using System.Threading.Tasks;

namespace Infrastructure.Identity.Interfaces
{
    public interface IUserManagerService
    {
        Task SendConfirmationEmail(string userEmail, string originUrl);
        Task SendResetPasswordEmail(string userEmail, string originUrl);
        Task<bool> VerifyResetPasswordTokenAsync(string userId, string token);
    }
}
