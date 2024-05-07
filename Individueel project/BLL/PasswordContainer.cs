using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCrypt;
using Microsoft.AspNetCore.Identity;

namespace BLL
{
    public class PasswordContainer
    {
        public string HashPassword(string password)
        {
            string salt = BCrypt.Net.BCrypt.GenerateSalt();

            var passwordHash =BCrypt.Net.BCrypt.HashPassword(password, salt);
            return passwordHash;
        }

        public bool Compare(string inputPassword, string passwordHash)
        {
            return BCrypt.Net.BCrypt.Verify(inputPassword, passwordHash);
        }
    }
}
