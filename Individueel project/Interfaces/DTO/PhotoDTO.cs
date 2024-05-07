

namespace Interfaces.DTOS
{
    public class PhotoDTO
    {
        public int? PhotoId { get; set; }
        public int UserId { get; set; }
        public string PhotoData { get; set; }

        
        public PhotoDTO() { }
    }
}
