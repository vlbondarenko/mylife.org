using System.Threading.Tasks;

namespace serverapp.Services
{
   
   public interface IUserService
   {
        Task<UserData> Login(LoginData loginData);
        Task<UserData> Register(RegisterData registerData);
   }

  
}
