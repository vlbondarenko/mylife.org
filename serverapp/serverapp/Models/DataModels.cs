namespace serverapp.Models
{
    public class UserData
    {
        public string DisplayName { get; set; }
        public string Id { get; set; }       
        public string Token { get; set; }
    }

    public class SignInData
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class SignUpData
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserEmail { get; set; }
        public string Password { get; set; }
    }

    public class ForgotPasswordData 
    { 
        public string Email { get; set; }
    }

    public class ResetPasswordData
    {
        public string NewPassword { get; set; }
    }

}
