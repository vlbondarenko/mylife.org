using System.Threading.Tasks;

namespace Infrastructure.Identity.Interfaces
{
    public interface IUserManagerService
    {
        Task SendConfirmationEmail(string userEmeil);
    }
}
