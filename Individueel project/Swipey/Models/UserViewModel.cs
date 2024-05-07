using BLL;

namespace Swipey.Models
{
    public class UserViewModel
    {
        public int? UserId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Biograpy { get; set; }
        public bool Gender { get; set; }
        public bool? PrefGender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Password { get; set; }
        public int Landcode { get; set; }
        public int PhoneNumber { get; set; }
        public bool Activated { get; set; }

        public List<PhotoViewModel>? Photos { get; set; } = new List<PhotoViewModel>();


    }
}
