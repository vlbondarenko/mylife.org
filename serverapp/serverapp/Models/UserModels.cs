using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace serverapp.Models
{
    public class UserModel
    {
        public string Name { get; set; }
        public string Email { get; set; }       
        public string Token { get; set; }
        public string Errors { get; set; }

    }

    public class LoginModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class RegisterModel
    {
        public string Email { get; set; }
        public string Nickname { get; set; }
        public string Password { get; set; }
    }
}
