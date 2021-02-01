using System.Threading.Tasks;
using serverapp.Models;

namespace serverapp.Services
{
   
   public interface IUserService
   {
        Task<UserModel> Login(LoginModel loginData);
        Task<UserModel> Register(RegisterModel registerData);
   }

  
}
