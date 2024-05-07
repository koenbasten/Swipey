using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Classes
{
    public class Photo
    {
        public int? PhotoId { get; set; }
        public int UserId { get; set; }
        public string PhotoData { get; set; }
    }
}
