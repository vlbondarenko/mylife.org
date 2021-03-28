using System.Threading.Tasks;

namespace Infrastructure.Identity.Interfaces
{
    public interface IUserManagerService
    {
        Task SendConfirmationEmail(string userEmail);
        Task SendResetPasswordEmail(string userEmail);
        Task<bool> VerifyResetPasswordTokenAsync(string userId, string token);
    }
}
