using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Classes
{
    public class AttemptedLogin
    {
        public int? AttemptedLoginId { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set;}
        //public IPAdress IpAdress { get; set;}
        public bool Success { get; set;}
        public string Description { get; set;}
    }
}
