using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BLL.Classes
{
    public class User
    {
        public int? UserId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string? Biograpy { get; set; }
        public bool? Gender { get; set; }
        public bool? PrefGender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public bool Activated {  get; set; }
    }
}