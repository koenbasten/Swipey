namespace Swipey.Models
{
    public class LoginViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Message { get; set; } = "";
        public bool NewUser { get; set; } = false;

    }
}
