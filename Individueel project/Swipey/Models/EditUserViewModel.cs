
namespace Swipey.Models
{
    public class EditUserViewModel
    { //aanpassen
        public string? UserName { get; set; }
        public string? Biograpy { get; set; }

        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public IFormFile Photo { get; set; }
        public string? UserErrorMessage { get; set; }
        public string? PhotoErrorMessage { get; set; }
        public List<PhotoViewModel>? Photos { get; set; } = new List<PhotoViewModel>();

    }
}
