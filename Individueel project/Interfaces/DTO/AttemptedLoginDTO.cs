using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.DTOS
{
    public class AttemptedLoginDTO
    {
        public int? AttemptedLoginId { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set;}
        //public IPAdress IpAdress { get; set;}
        public bool Success { get; set;}
        public string Description { get; set;}
    }
}
